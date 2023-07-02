using System;
using JetBrains.Annotations;
using UnityEngine;

namespace AiToolbox {
/// <summary>
/// The ChatGPT model to use.
/// Models are described here: https://platform.openai.com/docs/models/overview
/// </summary>
public enum Model {
    [InspectorName("gpt-3.5-turbo")]
    Gpt35Turbo = 0,
    [InspectorName("gpt-4")]
    Gpt4 = 1,
}

/// <summary>
/// The security and origin of the OpenAI API key.
/// </summary>
public enum ApiKeyEncryption {
    None = 0,
    [InspectorName("Locally encrypted")]
    LocallyEncrypted = 1,
    [InspectorName("RemoteConfig")]
    RemoteConfig = 2,
}

/// <summary>
/// Settings for the AI Toolbox ChatGPT requests.
/// </summary>
[Serializable]
public class Parameters : ISerializationCallbackReceiver {
    public string apiKey;
    public ApiKeyEncryption apiKeyEncryption;
    public string apiKeyRemoteConfigKey;
    public string apiKeyEncryptionPassword;

    public Model model;
    public float temperature;
    [CanBeNull]
    public string role;

    public int timeout;
    public int throttle;

    [SerializeField, HideInInspector]
    private bool serialized;

    public Parameters(string apiKey) {
        this.apiKey = apiKey;
    }

    public Parameters(Parameters parameters) {
        apiKey = parameters.apiKey;
        apiKeyEncryption = parameters.apiKeyEncryption;
        apiKeyRemoteConfigKey = parameters.apiKeyRemoteConfigKey;
        apiKeyEncryptionPassword = parameters.apiKeyEncryptionPassword;
        model = parameters.model;
        temperature = parameters.temperature;
        timeout = parameters.timeout;
        role = parameters.role;
        serialized = parameters.serialized;
        throttle = parameters.throttle;
    }

    public void OnBeforeSerialize() {
        if (serialized) return;
        serialized = true;
        temperature = 1;
        timeout = 0;
        throttle = 0;
        apiKeyRemoteConfigKey = "openai_api_key";
        apiKeyEncryptionPassword = Guid.NewGuid().ToString();
    }

    public void OnAfterDeserialize() { }
}
}