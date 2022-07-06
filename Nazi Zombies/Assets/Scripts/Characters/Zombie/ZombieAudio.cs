using UnityEngine;

namespace Zombie
{
	[RequireComponent(typeof(AudioSource))]
    public class ZombieAudio : MonoBehaviour
    {
        private AudioSource audioSource;
		private void Awake()
		{
			audioSource = this.GetComponent<AudioSource>();
		}


	}
}

