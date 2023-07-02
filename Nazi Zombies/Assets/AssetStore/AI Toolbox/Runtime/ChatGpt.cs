using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

// ReSharper disable InconsistentNaming
namespace AiToolbox {
public enum Role {
    User,
    AI,
}

public struct Message {
    public string text;
    public Role role;

    public Message(string text, Role role) {
        this.text = text;
        this.role = role;
    }
}

public enum ChatGptErrorCodes {
    MaxTokensExceeded = 0,
    ThrottleExceeded = 1,
    RemoteConfigConnectionFailure = 2,
    RemoteConfigKeyNotFound = 3,
    Unknown = 4,
}

internal sealed class RequestRecord {
    private Action cancelCallback;

    public void SetCancelCallback(Action callback) {
        cancelCallback = callback;
    }

    public void Cancel() {
        cancelCallback?.Invoke();
    }
}

public static class ChatGpt {
    private static readonly List<RequestRecord> _requestRecords = new List<RequestRecord>();

    /// <summary>
    /// Send a request to ChatGPT.
    /// </summary>
    /// <param name="prompt">The text of the request, e.g. "Generate a character description".</param>
    /// <param name="parameters">Settings of the request.</param>
    /// <param name="completeCallback">The function to be called on successful completion. ChatGPT response is provided
    /// as a parameter.</param>
    /// <param name="failureCallback">The function to be called on failure. Error code and message are provided as
    /// parameters.</param>
    /// <param name="updateCallback">The function to be called when a new response chunk is generated. ChatGPT response
    /// data is provided as a parameter.</param>
    /// <returns>A function that can be called to cancel the request.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static Action Request(string prompt, Parameters parameters, Action<string> completeCallback,
                                 Action<long, string> failureCallback, Action<string> updateCallback = null) {
        return Request(new List<Message> { new Message { role = Role.User, text = prompt } }, parameters,
                       completeCallback, failureCallback, updateCallback);
    }

    /// <summary>
    /// Send a request to ChatGPT.
    /// </summary>
    /// <param name="messages">Sequence of messages to send to ChatGPT. The order of messages should be the same as the
    /// chronological order of messages in the conversation, i.e. the first message should be the oldest one. The roles
    /// of the messages should switch between User and AI.</param> 
    /// <param name="parameters">Settings of the request.</param>
    /// <param name="completeCallback">The function to be called on successful completion. ChatGPT response is provided
    /// as a parameter.</param>
    /// <param name="failureCallback">The function to be called on failure. Error code and message are provided as
    /// parameters.</param>
    /// <param name="updateCallback">The function to be called when a new response chunk is generated. ChatGPT response
    /// data is provided as a parameter.</param>
    /// <returns>A function that can be called to cancel the request.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static Action Request(IEnumerable<Message> messages, Parameters parameters, Action<string> completeCallback,
                                 Action<long, string> failureCallback, Action<string> updateCallback = null) {
        Debug.Assert(parameters != null, "Parameters cannot be null.");
        Debug.Assert(!string.IsNullOrEmpty(parameters!.apiKey), "API key cannot be null or empty.");
        Debug.Assert(messages != null, "Messages cannot be null.");

        if (updateCallback == null) {
            return QuickRequest(messages, parameters, completeCallback, failureCallback);
        }

        // Throttle.
        if (parameters.throttle > 0) {
            var requestCount = _requestRecords.Count;
            if (requestCount >= parameters.throttle) {
                failureCallback?.Invoke((long)ChatGptErrorCodes.ThrottleExceeded,
                                        $"Too many requests. Maximum allowed: {parameters.throttle}.");
                return () => { };
            }
        }

        var requestRecord = new RequestRecord();
        var enumerator = Stream(messages, parameters, updateCallback, completeCallback, failureCallback, requestRecord);
        var cancelCallback = new Action(() => {
            if (enumerator != null) {
                ChatGptContainer.Instance.StopCoroutine(enumerator);
            }

            _requestRecords.Remove(requestRecord);
        });

        requestRecord.SetCancelCallback(cancelCallback);
        _requestRecords.Add(requestRecord);

        ChatGptContainer.Instance.StartCoroutine(enumerator);
        return cancelCallback;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// Cancel all pending requests.
    /// </summary>
    public static void CancelAllRequests() {
        while (_requestRecords.Count > 0) {
            _requestRecords[0].Cancel();
        }

        _requestRecords.Clear();
    }

    private static Action QuickRequest(IEnumerable<Message> messages, Parameters parameters,
                                       Action<string> completeCallback, Action<long, string> failureCallback) {
        if (parameters.apiKeyEncryption != ApiKeyEncryption.RemoteConfig) {
            return QuickRequestBlocking(messages, parameters, completeCallback, failureCallback);
        }

        var enumerator = QuickRequestCoroutine(messages, parameters, completeCallback, failureCallback);
        ChatGptContainer.Instance.StartCoroutine(enumerator);

        void CancelCallback() {
            ChatGptContainer.Instance.StopCoroutine(enumerator);
        }

        return CancelCallback;
    }

    private static IEnumerator QuickRequestCoroutine(IEnumerable<Message> messages, Parameters parameters,
                                                     Action<string> completeCallback,
                                                     Action<long, string> failureCallback) {
        if (parameters.apiKeyEncryption == ApiKeyEncryption.RemoteConfig) {
            yield return GetRemoteConfig(parameters, failureCallback);
        }

        QuickRequestBlocking(messages, parameters, completeCallback, failureCallback);
    }

    private static Action QuickRequestBlocking(IEnumerable<Message> messages, Parameters parameters,
                                               Action<string> completeCallback, Action<long, string> failureCallback) {
        Debug.Assert(parameters != null, "Parameters cannot be null.");
        Debug.Assert(!string.IsNullOrEmpty(parameters!.apiKey), "API key cannot be null or empty.");
        Debug.Assert(messages != null, "Messages cannot be null.");

        // Throttle.
        if (parameters.throttle > 0) {
            var requestCount = _requestRecords.Count;
            if (requestCount >= parameters.throttle) {
                failureCallback?.Invoke((long)ChatGptErrorCodes.ThrottleExceeded,
                                        $"Too many requests. Maximum allowed: {parameters.throttle}.");
                return () => { };
            }
        }

        var requestObject = new RequestMessage {
            model = GetModelName(parameters.model),
            temperature = parameters.temperature,
            stream = false,
            messages = ConvertMessages(messages, parameters.role),
        };

        var requestRecord = new RequestRecord();
        var requestJson = JsonUtility.ToJson(requestObject);
        var request = GetWebRequest(requestJson, parameters, failureCallback, requestRecord);
        var cancelCallback = new Action(() => {
            try {
                request?.Abort();
                request?.Dispose();
                _requestRecords.Remove(requestRecord);
            }
            catch (Exception) {
                // If the request is aborted, accessing the error property will throw an exception.
            }
        });
        requestRecord.SetCancelCallback(cancelCallback);
        _requestRecords.Add(requestRecord);

        request.SendWebRequest().completed += _ => {
            _requestRecords.Remove(requestRecord);
            Application.quitting -= cancelCallback;

            bool isErrorResponse;
            try {
                isErrorResponse = !string.IsNullOrEmpty(request.error);
            }
            catch (Exception) {
                // If the request is aborted, accessing the error property will throw an exception.
                return;
            }

            if (isErrorResponse) {
                failureCallback?.Invoke(request.responseCode, request.error);
                return;
            }

            var response = JsonUtility.FromJson<ResponseMessage>(request.downloadHandler.text);
            if (response.choices.Length == 0) {
                failureCallback?.Invoke((long)ChatGptErrorCodes.Unknown,
                                        "No response choices returned from the server.");
                return;
            }

            var responseMessage = response.choices[0].message.content;
            completeCallback?.Invoke(responseMessage);
            request.Dispose();
        };

        Application.quitting += cancelCallback;
        return cancelCallback;
    }

    private static IEnumerator Stream(IEnumerable<Message> messages, Parameters parameters,
                                      Action<string> updateCallback, Action<string> completeCallback,
                                      Action<long, string> failureCallback, RequestRecord requestRecord) {
        var requestObject = new RequestMessage {
            model = GetModelName(parameters.model),
            temperature = parameters.temperature,
            stream = true,
            messages = ConvertMessages(messages, parameters.role),
        };

        if (parameters.apiKeyEncryption == ApiKeyEncryption.RemoteConfig) {
            yield return GetRemoteConfig(parameters, failureCallback);
        }

        var requestJson = JsonUtility.ToJson(requestObject);
        using var request = GetWebRequest(requestJson, parameters, failureCallback, requestRecord);
        var webRequest = request.SendWebRequest();

        int textLength = 0;
        string completeText = "";

        while (!webRequest.isDone) {
            if (request.downloadHandler.text.Length > textLength) {
                if (!string.IsNullOrEmpty(request.error)) {
                    failureCallback(request.responseCode, request.error);
                    _requestRecords.Remove(requestRecord);
                    yield break;
                }

                var text = request.downloadHandler.text;
                var newText = text.Substring(textLength);
                textLength = text.Length;
                while (newText.Contains("data: ")) {
                    var startTrimmed =
                        newText.Substring(newText.IndexOf("data: ", StringComparison.Ordinal) + "data: ".Length);
                    var dataEndPosition = startTrimmed.IndexOf("data: ", StringComparison.Ordinal);
                    var dataJson = dataEndPosition == -1 ? startTrimmed : startTrimmed.Substring(0, dataEndPosition);
                    newText = dataEndPosition == -1 ? "" : startTrimmed.Substring(dataEndPosition);
                    if (dataJson.Contains("[DONE]")) {
                        break;
                    }

                    try {
                        var data = JsonUtility.FromJson<ResponseMessage>(dataJson);

                        if (data.choices == null || data.choices.Length == 0) {
                            failureCallback((long)ChatGptErrorCodes.Unknown,
                                            "No response choices returned from the server.");
                            _requestRecords.Remove(requestRecord);
                            yield break;
                        }

                        if (data.choices[0].finish_reason == "length") {
                            failureCallback((long)ChatGptErrorCodes.MaxTokensExceeded, completeText);
                            _requestRecords.Remove(requestRecord);
                            yield break;
                        }

                        var delta = data.choices[0].delta.content;
                        completeText += delta;
                        updateCallback?.Invoke(delta);
                    }
                    catch (Exception e) {
                        failureCallback((long)ChatGptErrorCodes.Unknown, e.Message);
                        _requestRecords.Remove(requestRecord);
                        yield break;
                    }
                }
            }

            yield return null;
        }

        if (!string.IsNullOrEmpty(request.error)) {
            failureCallback?.Invoke(request.responseCode, request.error);
            _requestRecords.Remove(requestRecord);
            yield break;
        }

        if (!string.IsNullOrEmpty(completeText)) {
            completeCallback?.Invoke(completeText);
            _requestRecords.Remove(requestRecord);
        }
    }

    private static IEnumerator GetRemoteConfig(Parameters parameters, Action<long, string> failureCallback) {
        var apiKeySet = false;
        var task = RemoteKeyService.GetOpenAiKey(parameters.apiKeyRemoteConfigKey, s => {
            parameters.apiKeyEncryption = ApiKeyEncryption.None;
            parameters.apiKey = s;
            apiKeySet = true;
        }, (errorCode, error) => {
            failureCallback?.Invoke(errorCode, error);
            apiKeySet = true;
        });

        yield return new WaitUntil(() => task.IsCompleted && apiKeySet);

        if (task.IsFaulted) {
            failureCallback?.Invoke((long)ChatGptErrorCodes.RemoteConfigConnectionFailure,
                                    "Failed to retrieve API key from remote config.");
        }
    }

    private static UnityWebRequest GetWebRequest(string requestJson, Parameters parameters,
                                                 Action<long, string> failureCallback, RequestRecord requestRecord) {
        var baseUrl = "https://api.openai.com/v1/chat/completions";
#if UNITY_2022_2_OR_NEWER
        var request = UnityWebRequest.Post(baseUrl, requestJson, "application/json");
#else
        var request = new UnityWebRequest(baseUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(requestJson));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
#endif

        request.timeout = parameters.timeout;

        try {
            var apiKey = parameters.apiKey;
            var isEncrypted = parameters.apiKeyEncryption == ApiKeyEncryption.LocallyEncrypted;
            if (isEncrypted) {
                apiKey = Key.B(apiKey, parameters.apiKeyEncryptionPassword);
            }

            request.SetRequestHeader("Authorization", "Bearer " + apiKey);
        }
        catch (Exception e) {
            failureCallback?.Invoke((long)ChatGptErrorCodes.Unknown, e.Message);
            _requestRecords.Remove(requestRecord);
        }

        return request;
    }

    private static string GetModelName(Model model) {
        return model switch {
            Model.Gpt35Turbo => "gpt-3.5-turbo",
            Model.Gpt4 => "gpt-4",
            _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
        };
    }

    private static RoleContentMessage[] ConvertMessages(IEnumerable<Message> messages, string role) {
        var systemMessageOffset = string.IsNullOrEmpty(role) ? 0 : 1;
        var inputArray = messages as Message[] ?? messages.ToArray();
        var requestMessages = new RoleContentMessage[inputArray.Length + systemMessageOffset];

        if (systemMessageOffset > 0) {
            requestMessages[0] = new RoleContentMessage { role = "system", content = role };
        }

        for (var i = systemMessageOffset; i < requestMessages.Length; i++) {
            var message = inputArray[i - systemMessageOffset];
            requestMessages[i] = new RoleContentMessage {
                role = message.role == Role.User ? "user" : "assistant", content = message.text
            };
        }

        return requestMessages;
    }

    private class ChatGptContainer : MonoBehaviour {
        private static ChatGptContainer _instance;
        internal static ChatGptContainer Instance {
            get {
                if (_instance == null) {
                    var container = new GameObject("ChatGptContainer");
                    DontDestroyOnLoad(container);
                    container.hideFlags = HideFlags.HideInHierarchy;
                    _instance = container.AddComponent<ChatGptContainer>();
                }

                return _instance;
            }
        }

        private void OnApplicationQuit() {
            CancelAllRequests();
        }
    }

#pragma warning disable 0649
// ReSharper disable NotAccessedField.Local
    [Serializable]
    private struct RequestMessage {
        public string model;
        public RoleContentMessage[] messages;
        public float temperature;
        public bool stream;
        // Omitted fields: int n, string stop, int max_tokens,
        // float presence_penalty, float frequency_penalty;
    }

    [Serializable]
    private struct RoleContentMessage {
        public string role;
        public string content;
    }

    [Serializable]
    private struct ResponseMessage {
        public string id;
        public string created;
        public ResponseChoice[] choices;
        public string model;
        public Usage usage;
    }

    [Serializable]
    private struct ResponseChoice {
        public int index;
        public RoleContentMessage delta;
        public RoleContentMessage message;
        public string finish_reason;
    }

    [Serializable]
    private struct Usage {
        public int completion_tokens;
        public int prompt_tokens;
        public int total_tokens;
    }
}
}