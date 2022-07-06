
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour
{
	public AudioSource audioSource { get; private set; }

	private void Awake()
	{
		audioSource = this.GetComponent<AudioSource>();
	}
}
