using UnityEngine;

namespace AiToolboxRuntimeSample {
public class OpenHyperlink : MonoBehaviour {
    [SerializeField, Tooltip("The hyperlink to open when the function is called.")]
    private string hyperlink = "https://ai-toolbox.dustyroom.com";

    public void OpenLink() {
        Application.OpenURL(hyperlink);
    }
}
}