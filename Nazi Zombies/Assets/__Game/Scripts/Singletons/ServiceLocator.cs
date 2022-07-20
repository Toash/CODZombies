using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
	public static ServiceLocator Instance { get; private set; } 
	public AudioManager AudioManager { get; private set; }
	public UIManager UIManager { get; private set; }
	public InputManager InputManager { get; private set; }
	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(this);
		AudioManager = GetComponentInChildren<AudioManager>();
		UIManager = GetComponentInChildren<UIManager>();
		InputManager = GetComponentInChildren<InputManager>();
	}

}
