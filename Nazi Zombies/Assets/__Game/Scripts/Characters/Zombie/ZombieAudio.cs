using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ZombieAudio : MonoBehaviour
{
    [SerializeField]
    private AudioClip hitSound;
    [SerializeField]
    private AudioClip deathSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayHitSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = hitSound;
            audioSource.Play();
        }
    }

    public void PlayDeathSound()
    {
        audioSource.clip = deathSound;
        audioSource.Play();
    }

}
