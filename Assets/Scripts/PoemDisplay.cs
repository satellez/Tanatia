using UnityEngine;
using TMPro;

public class PoemDisplay : MonoBehaviour
{
    public GameObject poemPanel; // Panel que muestra el poema
    private bool hasSeenPoem = false; // Variable para controlar si el poema ya fue visto
    private float displayDuration = 15f; // Duración en segundos para mostrar el poema
    private float timer = 0f; // Temporizador interno
    private bool isPlayerInside = false; // Controla si el jugador está en la zona de lectura

    private void Start()
    {
        poemPanel.SetActive(false); // Asegura que el panel esté oculto al inicio
    }

    private void Update()
    {
        // Solo incrementa el temporizador si el jugador está en la zona y el poema no ha sido visto antes
        if (isPlayerInside && !hasSeenPoem)
        {
            timer += Time.deltaTime;
            if (timer >= displayDuration) // Si se cumple el tiempo de visualización
            {
                HidePoem(); // Oculta el poema y marca como visto
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("personaje") && !hasSeenPoem) // Solo muestra el poema si no ha sido visto
        {
            ShowPoem();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("personaje") && !hasSeenPoem) // Si el jugador sale antes de tiempo, oculta el poema
        {
            HidePoem();
        }
    }

    private void ShowPoem()
    {
        poemPanel.SetActive(true); // Muestra el panel con el poema
        isPlayerInside = true; // Marca que el jugador está en la zona
        timer = 0f; // Reinicia el temporizador al entrar
    }

    private void HidePoem()
    {
        poemPanel.SetActive(false); // Oculta el panel
        isPlayerInside = false; // Marca que el jugador ha salido de la zona
        hasSeenPoem = true; // Marca el poema como visto, para que no se vuelva a mostrar
    }
}
