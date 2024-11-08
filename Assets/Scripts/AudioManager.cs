using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip screamAudio; // Tu archivo MP3
    private AudioSource audioSource;

    void Start()
    {
        // Obtén el AudioSource del objeto al que esté asignado
        audioSource = GetComponent<AudioSource>();

        // Asigna el audio clip al AudioSource
        audioSource.clip = screamAudio;

        // Reproduce el sonido automáticamente
        audioSource.Play();
    }
}
