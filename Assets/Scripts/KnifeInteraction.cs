using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Si estás usando TextMeshPro para los textos de UI

public class KnifeInteraction : MonoBehaviour
{
    public GameObject messageText; // Texto inicial para mostrar al usuario
    private bool isMessageDisplayed = true;
    private float messageDuration = 5f; // Duración del mensaje inicial en segundos

    private void Start()
    {
        // Asegúrate de que el mensaje inicial esté activo al comienzo
        if (messageText != null)
        {
            messageText.SetActive(true);
            Invoke("HideInitialMessage", messageDuration); // Oculta el mensaje después de messageDuration segundos
        }
    }

    private void HideInitialMessage()
    {
        if (messageText != null)
        {
            messageText.SetActive(false);
            isMessageDisplayed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el jugador interactúa con el cuchillo
        if (other.CompareTag("personaje"))
        {
            // Cambia a la escena "Biblio1" cuando el jugador encuentra el cuchillo
            SceneManager.LoadScene("Biblio1");
        }
    }
}
