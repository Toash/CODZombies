using System;
using System.Threading.Tasks;
#if REMOTE_CONFIG_IMPORTED
using UnityEngine;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
#endif

namespace AiToolbox {
internal static class RemoteKeyService {
#if REMOTE_CONFIG_IMPORTED
    private struct UserAttributes { }

    private struct AppAttributes { }
#endif

    internal static async Task GetOpenAiKey(string remoteConfigKey, Action<string> successCallback,
                                            Action<long, string> failureCallback) {
#if REMOTE_CONFIG_IMPORTED
        if (string.IsNullOrEmpty(remoteConfigKey)) {
            failureCallback?.Invoke((long)ChatGptErrorCodes.RemoteConfigConnectionFailure,
                                    "RemoteConfig key is null or empty.");
            return;
        }

        if (Utilities.CheckForInternetConnection()) {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn) {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }

        void PublishKey(ConfigResponse configResponse) {
            var apiKey = RemoteConfigService.Instance.appConfig.config[remoteConfigKey];
            if (apiKey == null) {
                failureCallback?.Invoke((long)ChatGptErrorCodes.RemoteConfigKeyNotFound,
                                        "RemoteConfig did not contain the field " + remoteConfigKey);
                return;
            }

            successCallback?.Invoke(apiKey.ToString());
        }

        RemoteConfigService.Instance.FetchCompleted += PublishKey;
        RemoteConfigService.Instance.FetchConfigs(new UserAttributes(), new AppAttributes());

        Application.quitting += () => { RemoteConfigService.Instance.FetchCompleted -= PublishKey; };
#else
        failureCallback?.Invoke((long)ChatGptErrorCodes.RemoteConfigConnectionFailure,
                                "RemoteConfig package is not imported. Please import it from the Package Manager: " +
                                "Unity Registry > Remote Config.");
        await Task.CompletedTask;
#endif
    }
}
}