using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public ScorePopup scorePopup;
    public int scoreToReach = 1000; // Puntaje objetivo
    public int requiredBooks = 5; // Libros necesarios
    public APIManager2 apiManager; // Referencia a APIManager
    private int currentScore = 0;
    private int booksCollected = 0;

    // Método que se llama cuando se recoge un libro
    public void BookCollected()
    {
        booksCollected++;
        AddScore(200); // Asumimos que cada libro da 200 puntos

        // Verificamos si el jugador alcanzó los 5 libros y los 1000 puntos
        if (currentScore >= scoreToReach && booksCollected >= requiredBooks)
        {
            // Llamar al APIManager para enviar los datos del checkpoint
            apiManager.SendBookData("CheckpointCementerio");

            // Cambiar de escena o realizar otra acción
            LoadNextScene();
        }
    }

    // Función para sumar el puntaje
    void AddScore(int points)
    {
        currentScore += points;
        Debug.Log("Puntaje añadido: " + points + ". Puntaje actual: " + currentScore);

        // Muestra el popup con el puntaje total
        if (scorePopup != null)
        {
            scorePopup.Show(currentScore);
        }
    }

    // Cambiar de escena
    void LoadNextScene()
    {
        // Asegúrate de tener una escena llamada "Hospital" o la que desees cargar
        SceneManager.LoadScene("Hospital"); // O usa el nombre adecuado de tu escena
    }
}
