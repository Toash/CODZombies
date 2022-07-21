using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public void PlaySoundAtLocation(AudioClip sound, Vector3 location)
	{
		AudioSource a = Instantiate(new AudioSource(),location,Quaternion.identity);
		a.PlayOneShot(sound);
		Destroy(a.gameObject, sound.length);
	}
}
