using UnityEngine;

public class BaseAudio : MonoBehaviour
{
	protected AudioSource audioSource;
	protected void Awake()
	{
		audioSource = this.GetComponent<AudioSource>();
	}
}
