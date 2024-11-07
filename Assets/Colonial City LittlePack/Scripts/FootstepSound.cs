using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip[] footstepSounds; // Lista de sonidos de pasos
    public AudioSource audioSource; // Audio Source del personaje
    private CharacterController characterController; // Controlador del personaje

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Reproduce sonido solo si el personaje se está moviendo y está en el suelo
        if (characterController.isGrounded && characterController.velocity.magnitude > 0.1f && !audioSource.isPlaying)
        {
            PlayFootstepSound();
        }
    }

    void PlayFootstepSound()
    {
        int randomIndex = Random.Range(0, footstepSounds.Length);
        audioSource.clip = footstepSounds[randomIndex];
        audioSource.Play();
    }
}
