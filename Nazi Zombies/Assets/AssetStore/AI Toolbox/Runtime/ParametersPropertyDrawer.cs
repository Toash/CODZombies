#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace AiToolbox {
[CustomPropertyDrawer(typeof(Parameters))]
public class ParametersPropertyDrawer : PropertyDrawer {
    private float _height;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        label.text = "ChatGPT Parameters";
        label.tooltip = "Settings for the ChatGPT API.";
        EditorGUI.BeginProperty(position, label, property);

        property.isExpanded =
            EditorGUI.Foldout(new Rect(position.x, position.y, position.width - 20, EditorGUIUtility.singleLineHeight),
                              property.isExpanded, label, true);
        _height = EditorGUIUtility.singleLineHeight;

        // Question mark button
        {
            var buttonRect = new Rect(position.x + position.width - 20, position.y + 2, 20,
                                      EditorGUIUtility.singleLineHeight);
            var icon = EditorGUIUtility.IconContent("_Help");
            icon.tooltip = "Open documentation";
            if (GUI.Button(buttonRect, icon, EditorStyles.label)) {
                Application.OpenURL("https://ai-toolbox.dustyroom.com/runtime");
            }
        }

        if (property.isExpanded) {
            EditorGUI.indentLevel++;
            var width = position.width - EditorGUIUtility.standardVerticalSpacing * 2f;

            // Space
            {
                var rect = new Rect(position.x, position.y + _height, width, EditorGUIUtility.standardVerticalSpacing);
                EditorGUI.LabelField(rect, GUIContent.none);
                _height += rect.height + EditorGUIUtility.standardVerticalSpacing;
            }

            // API Key
            {
                var apiKeyProperty = property.FindPropertyRelative(nameof(Parameters.apiKey));
                var apiKey = apiKeyProperty.stringValue;
                if (string.IsNullOrEmpty(apiKey)) {
                    const string m = "API Key is required.";
                    var helpBoxHeight = EditorStyles.helpBox.CalcHeight(new GUIContent(m),
                                                                        width - EditorGUIUtility.labelWidth - 24);
                    var helpBoxRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y + _height,
                                               width - EditorGUIUtility.labelWidth, helpBoxHeight);
                    EditorGUI.HelpBox(helpBoxRect, m, MessageType.Error);
                    _height += helpBoxRect.height + EditorGUIUtility.standardVerticalSpacing;

                    var buttonRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y + _height,
                                              width - EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
                    _height += buttonRect.height + EditorGUIUtility.standardVerticalSpacing;
                    if (GUI.Button(buttonRect, "Open API Keys page", EditorStyles.miniButton)) {
                        Application.OpenURL("https://platform.openai.com/account/api-keys");
                    }
                }

                var rect = new Rect(position.x, position.y + _height, width, EditorGUIUtility.singleLineHeight);
                _height += rect.height + EditorGUIUtility.standardVerticalSpacing;
                var encryptionProperty = property.FindPropertyRelative(nameof(Parameters.apiKeyEncryption));
                var isRemote = encryptionProperty.enumValueIndex == (int)ApiKeyEncryption.RemoteConfig;
                var apiKeyTitle = isRemote ? "API Key (fallback)" : "API Key";
                var apiKeyLabel = new GUIContent(apiKeyTitle, "Your API Key from the OpenAI platform.");
                var isEncrypted = encryptionProperty.enumValueIndex == (int)ApiKeyEncryption.LocallyEncrypted;
                var passwordProperty = property.FindPropertyRelative(nameof(Parameters.apiKeyEncryptionPassword));
                if (isEncrypted && !string.IsNullOrEmpty(apiKey) && !apiKey.StartsWith("sk-") &&
                    !string.IsNullOrEmpty(passwordProperty.stringValue)) {
                    apiKey = Key.B(apiKey, passwordProperty.stringValue);
                }

                var apiKeyEdited = EditorGUI.TextField(rect, apiKeyLabel, apiKey);
                if (apiKeyEdited != apiKey) {
                    apiKeyProperty.stringValue = isEncrypted && !string.IsNullOrEmpty(apiKeyEdited)
                        ? Key.A(apiKeyEdited, passwordProperty.stringValue)
                        : apiKeyEdited;
                }
            }

            // API Key encryption
            {
                EditorGUI.indentLevel++;
                var encryptionProperty = property.FindPropertyRelative(nameof(Parameters.apiKeyEncryption));

                // Warning
                if (encryptionProperty.enumValueIndex == (int)ApiKeyEncryption.None &&
                    !string.IsNullOrEmpty(property.FindPropertyRelative(nameof(Parameters.apiKey)).stringValue)) {
                    const string m = "API Key is not encrypted. A malicious actor could steal your API Key by " +
                                     "decompiling your build.";
                    var helpBoxHeight = EditorStyles.helpBox.CalcHeight(new GUIContent(m),
                                                                        width - EditorGUIUtility.labelWidth - 24);
                    var helpBoxRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y + _height,
                                               width - EditorGUIUtility.labelWidth, helpBoxHeight);
                    EditorGUI.HelpBox(helpBoxRect, m, MessageType.Warning);
                    _height += helpBoxRect.height + EditorGUIUtility.standardVerticalSpacing;
                }

                var rect = new Rect(position.x, position.y + _height, width, EditorGUIUtility.singleLineHeight);
                _height += rect.height + EditorGUIUtility.standardVerticalSpacing;
                var encryptionLabel = new GUIContent("Encryption",
                                                     "Encrypt API Key to prevent malicious actors from stealing it.");
                EditorGUI.BeginChangeCheck();
                EditorGUI.PropertyField(rect, encryptionProperty, encryptionLabel);
                if (EditorGUI.EndChangeCheck()) {
                    var apiKeyProperty = property.FindPropertyRelative(nameof(Parameters.apiKey));
                    var passwordProperty = property.FindPropertyRelative(nameof(Parameters.apiKeyEncryptionPassword));
                    var isEncrypted = encryptionProperty.enumValueIndex == (int)ApiKeyEncryption.LocallyEncrypted;
                    if (isEncrypted) {
                        if (string.IsNullOrEmpty(passwordProperty.stringValue)) {
                            passwordProperty.stringValue = Guid.NewGuid().ToString();
                        }

                        if (apiKeyProperty.stringValue.StartsWith("sk-") &&
                            !string.IsNullOrEmpty(apiKeyProperty.stringValue)) {
                            var encryptedApiKey = Key.A(apiKeyProperty.stringValue, passwordProperty.stringValue);
                            apiKeyProperty.stringValue = encryptedApiKey;
                        }
                    } else {
                        if (!apiKeyProperty.stringValue.StartsWith("sk-") &&
                            !string.IsNullOrEmpty(passwordProperty.stringValue) &&
                            !string.IsNullOrEmpty(apiKeyProperty.stringValue)) {
                            var decryptedApiKey = Key.B(apiKeyProperty.stringValue, passwordProperty.stringValue);
                            apiKeyProperty.stringValue = decryptedApiKey;
                        }
                    }
                }

                var showPasswordField = encryptionProperty.enumValueIndex == (int)ApiKeyEncryption.LocallyEncrypted;
                if (showPasswordField) {
                    var passwordProperty = property.FindPropertyRelative(nameof(Parameters.apiKeyEncryptionPassword));

                    if (string.IsNullOrEmpty(passwordProperty.stringValue)) {
                        const string m = "Password is required.";
                        var helpBoxHeight = EditorStyles.helpBox.CalcHeight(new GUIContent(m),
                                                                            width - EditorGUIUtility.labelWidth - 24);
                        var helpBoxRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y + _height,
                                                   width - EditorGUIUtility.labelWidth, helpBoxHeight);
                        EditorGUI.HelpBox(helpBoxRect, m, MessageType.Error);
                        _height += helpBoxRect.height + EditorGUIUtility.standardVerticalSpacing;
                    }

                    var passwordRect = new Rect(position.x, position.y + _height, width,
                                                EditorGUIUtility.singleLineHeight);
                    var passwordLabel = new GUIContent("Password", "Password used to encrypt API Key.");
                    var passwordEdited = EditorGUI.TextField(passwordRect, passwordLabel, passwordProperty.stringValue);
                    if (passwordEdited != passwordProperty.stringValue) {
                        var apiKeyProperty = property.FindPropertyRelative(nameof(Parameters.apiKey));
                        var decryptedApiKey = string.IsNullOrEmpty(passwordProperty.stringValue)
                            ? apiKeyProperty.stringValue
                            : Key.B(apiKeyProperty.stringValue, passwordProperty.stringValue);
                        var encryptedApiKey = string.IsNullOrEmpty(passwordEdited)
                            ? decryptedApiKey
                            : Key.A(decryptedApiKey, passwordEdited);
                        apiKeyProperty.stringValue = encryptedApiKey;
                        passwordProperty.stringValue = passwordEdited;
                    }

                    _height += passwordRect.height + EditorGUIUtility.standardVerticalSpacing;
                }

                var showRemoteConfigField = encryptionProperty.enumValueIndex == (int)ApiKeyEncryption.RemoteConfig;
                if (showRemoteConfigField) {
                    var remoteConfigKeyProperty =
                        property.FindPropertyRelative(nameof(Parameters.apiKeyRemoteConfigKey));
                    var remoteConfigKeyRect = new Rect(position.x, position.y + _height, width,
                                                       EditorGUIUtility.singleLineHeight);
                    EditorGUI.PropertyField(remoteConfigKeyRect, remoteConfigKeyProperty,
                                            new GUIContent("Remote Config Key"));
                    _height += remoteConfigKeyRect.height + EditorGUIUtility.standardVerticalSpacing;
                }

                EditorGUI.indentLevel--;
            }

            // Space
            {
                var rect = new Rect(position.x, position.y + _height, width, EditorGUIUtility.standardVerticalSpacing);
                EditorGUI.LabelField(rect, GUIContent.none);
                _height += rect.height + EditorGUIUtility.standardVerticalSpacing;
            }

            // Model
            {
                var modelProperty = property.FindPropertyRelative(nameof(Parameters.model));
                var rect = new Rect(position.x, position.y + _height, width, EditorGUIUtility.singleLineHeight);
                var modelLabel = new GUIContent("Model", "The ChatGPT model to use.");
                EditorGUI.PropertyField(rect, modelProperty, modelLabel);
                _height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                if (modelProperty.enumValueIndex == (int)Model.Gpt4) {
                    const string m = "GPT-4 is currently in a limited beta and only accessible to those who have " +
                                     "been granted access.";
                    var helpBoxHeight = EditorStyles.helpBox.CalcHeight(new GUIContent(m),
                                                                        width - EditorGUIUtility.labelWidth - 24);
                    var helpBoxRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y + _height,
                                               width - EditorGUIUtility.labelWidth, helpBoxHeight);
                    EditorGUI.HelpBox(helpBoxRect, m, MessageType.Warning);
                    _height += helpBoxRect.height + EditorGUIUtility.standardVerticalSpacing;

                    var buttonRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y + _height,
                                              width - EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
                    if (GUI.Button(buttonRect, "Request access", EditorStyles.miniButton)) {
                        Application.OpenURL("https://openai.com/waitlist/gpt-4-api");
                    }

                    _height += buttonRect.height + EditorGUIUtility.standardVerticalSpacing;
                }
            }

            // Temperature
            {
                var rect = new Rect(position.x, position.y + _height, width, EditorGUIUtility.singleLineHeight);
                var temperatureLabel = new GUIContent("Temperature",
                                                      "Controls the randomness of the model. Lower values make the " +
                                                      "model more predictable; higher values make the model more " +
                                                      "surprising.");
                EditorGUI.Slider(rect, property.FindPropertyRelative("temperature"), 0f, 1f, temperatureLabel);
                _height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            // Role
            {
                var roleProperty = property.FindPropertyRelative(nameof(Parameters.role));
                var useRole = !string.IsNullOrEmpty(roleProperty.stringValue);
                var toggleRect = new Rect(position.x, position.y + _height, EditorGUIUtility.labelWidth,
                                          EditorGUIUtility.singleLineHeight);

                EditorGUI.BeginChangeCheck();
                var roleLabel = new GUIContent(" Role", "The character or entity the AI will assume the role of.");
                useRole = EditorGUI.ToggleLeft(toggleRect, roleLabel, useRole);
                if (EditorGUI.EndChangeCheck()) {
                    if (useRole) {
                        roleProperty.stringValue = "Character Description";
                    }

                    if (!useRole) {
                        roleProperty.stringValue = null;
                    }
                }

                var rect = new Rect(position.x + EditorGUIUtility.labelWidth - 15, position.y + _height,
                                    width - EditorGUIUtility.labelWidth + 15, EditorGUIUtility.singleLineHeight);

                EditorGUI.BeginDisabledGroup(!useRole);

                if (useRole) {
                    EditorGUI.BeginChangeCheck();
                    EditorGUI.PropertyField(rect, roleProperty, GUIContent.none);
                    if (EditorGUI.EndChangeCheck()) {
                        if (roleProperty.stringValue == string.Empty) {
                            roleProperty.stringValue = " ";
                        }
                    }
                } else {
                    EditorGUI.LabelField(rect, "Character Description", EditorStyles.textField);
                }

                EditorGUI.EndDisabledGroup();

                _height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            // Timeout
            {
                var timeoutProperty = property.FindPropertyRelative(nameof(Parameters.timeout));
                var useTimeout = timeoutProperty.intValue > 0;
                var toggleRect = new Rect(position.x, position.y + _height, EditorGUIUtility.labelWidth,
                                          EditorGUIUtility.singleLineHeight);

                EditorGUI.BeginChangeCheck();
                var timeoutLabel = new GUIContent(" Timeout", "The maximum number of seconds to wait for a response.");
                useTimeout = EditorGUI.ToggleLeft(toggleRect, timeoutLabel, useTimeout);
                if (EditorGUI.EndChangeCheck()) {
                    if (useTimeout && timeoutProperty.intValue == 0) {
                        timeoutProperty.intValue = 60;
                    }

                    if (!useTimeout && timeoutProperty.intValue > 0) {
                        timeoutProperty.intValue = 0;
                    }
                }

                var rect = new Rect(position.x + EditorGUIUtility.labelWidth - 15, position.y + _height,
                                    width - EditorGUIUtility.labelWidth + 15, EditorGUIUtility.singleLineHeight);

                EditorGUI.BeginDisabledGroup(!useTimeout);

                if (useTimeout) {
                    EditorGUI.BeginChangeCheck();
                    EditorGUI.PropertyField(rect, timeoutProperty, GUIContent.none);
                    if (EditorGUI.EndChangeCheck()) {
                        if (timeoutProperty.intValue < 1) {
                            timeoutProperty.intValue = 1;
                        }
                    }
                } else {
                    EditorGUI.LabelField(rect, "Disabled", EditorStyles.textField);
                }

                EditorGUI.EndDisabledGroup();

                _height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            // Throttle
            {
                var throttleProperty = property.FindPropertyRelative(nameof(Parameters.throttle));
                var useThrottle = throttleProperty.intValue > 0;
                var toggleRect = new Rect(position.x, position.y + _height, EditorGUIUtility.labelWidth,
                                          EditorGUIUtility.singleLineHeight);

                EditorGUI.BeginChangeCheck();
                var throttleLabel = new GUIContent(" Throttle", "The maximum number of concurrent requests.");
                useThrottle = EditorGUI.ToggleLeft(toggleRect, throttleLabel, useThrottle);
                if (EditorGUI.EndChangeCheck()) {
                    if (useThrottle && throttleProperty.intValue == 0) {
                        throttleProperty.intValue = 10;
                    }

                    if (!useThrottle && throttleProperty.intValue > 0) {
                        throttleProperty.intValue = 0;
                    }
                }

                var rect = new Rect(position.x + EditorGUIUtility.labelWidth - 15, position.y + _height,
                                    width - EditorGUIUtility.labelWidth + 15, EditorGUIUtility.singleLineHeight);

                EditorGUI.BeginDisabledGroup(!useThrottle);

                if (useThrottle) {
                    EditorGUI.BeginChangeCheck();
                    EditorGUI.PropertyField(rect, throttleProperty, GUIContent.none);
                    if (EditorGUI.EndChangeCheck()) {
                        if (throttleProperty.intValue < 1) {
                            throttleProperty.intValue = 1;
                        }
                    }
                } else {
                    EditorGUI.LabelField(rect, "Disabled", EditorStyles.textField);
                }

                EditorGUI.EndDisabledGroup();

                _height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            // Draw box around parameters
            {
                var indentWidth = EditorGUI.indentLevel * 15 - 5;
                var rect = new Rect(position.x + indentWidth, position.y + EditorGUIUtility.singleLineHeight,
                                    position.width - indentWidth,
                                    _height - EditorGUIUtility.singleLineHeight +
                                    EditorGUIUtility.standardVerticalSpacing);
                GUI.backgroundColor = Color.gray;
                EditorGUI.HelpBox(rect, null, MessageType.None);
            }

            // Space
            {
                var rect = new Rect(position.x, position.y + _height, width, EditorGUIUtility.standardVerticalSpacing);
                EditorGUI.LabelField(rect, GUIContent.none);
                _height += rect.height + EditorGUIUtility.standardVerticalSpacing * 2;
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
        property.serializedObject.ApplyModifiedProperties();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return _height;
    }
}
}

#endif