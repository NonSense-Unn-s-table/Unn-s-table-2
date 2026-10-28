using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TeleportAudio : MonoBehaviour
{
    private AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayRandomPitchSound()
    {
        if (audioSource == null) return;
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }
}
