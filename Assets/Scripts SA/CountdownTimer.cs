using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 120f; // Tiempo total en segundos (2 minutos)
    public Text timerText; // Referencia al UI Text que muestra el tiempo restante
    public Image[] screamerImages; // Lista de imágenes de screamers
    public GameObject gameOverScreen; // Pantalla de Game Over

    private bool gameEnded = false; // Verificar si el juego ha terminado
    private float[] criticalTimes = { 90f, 75f, 50f, 30f, 10f, 1f }; // Tiempos críticos
    private bool[] screamerShown; // Para evitar mostrar el screamer varias veces

    void Start()
    {
        screamerShown = new bool[criticalTimes.Length];
        foreach (var image in screamerImages)
        {
            image.enabled = false; // Asegura que todas las imágenes estén ocultas al principio
        }
        gameOverScreen.SetActive(false); // Ocultar la pantalla de Game Over al principio
    }

    void Update()
    {
        if (gameEnded) return;

        // Reducir el tiempo
        countdownTime -= Time.deltaTime;

        // Actualizar el texto del temporizador
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Verificar si se debe mostrar un screamer en los tiempos críticos
        for (int i = 0; i < criticalTimes.Length; i++)
        {
            if (countdownTime <= criticalTimes[i] && !screamerShown[i])
            {
                StartCoroutine(ShowScreamer(i));
                screamerShown[i] = true;
            }
        }

        // Verificar si el tiempo se acabó
        if (countdownTime <= 0)
        {
            EndGame();
        }
    }

    // Muestra el screamer en el índice especificado por un segundo
    IEnumerator ShowScreamer(int index)
    {
        if (index < screamerImages.Length) // Asegura que el índice sea válido
        {
            screamerImages[index].enabled = true;
            yield return new WaitForSeconds(1f);
            screamerImages[index].enabled = false;
        }
    }

    // Manejar el final del juego
    void EndGame()
    {
        gameEnded = true;
        timerText.enabled = false;
        gameOverScreen.SetActive(true); // Mostrar la pantalla de Game Over
    }
}
