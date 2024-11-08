using UnityEngine;

public class Basurero : MonoBehaviour
{
    public ScorePopup scorePopup; // Asegúrate de asignar este campo en el Inspector
    public ScoreManager scoreManager; // Referencia a ScoreManager
    private int currentScore = 0; // Variable para llevar el puntaje actual

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona es un libro
        if (other.CompareTag("Book"))
        {
            // Destruye el libro
            Destroy(other.gameObject);

            // Añade puntaje y muestra el popup
            AddScore(200); // Agrega 200 puntos al puntaje
            ShowScorePopup(); // Muestra el popup con el puntaje total
            scoreManager.BookCollected(); // Notifica a ScoreManager que un libro fue recogido
        }
    }

    // Función para sumar el puntaje
    void AddScore(int points)
    {
        currentScore += points; // Suma los puntos al puntaje actual
        Debug.Log("Puntaje añadido: " + points + ". Puntaje actual: " + currentScore); // Depuración
    }

    // Función para mostrar el popup con el puntaje total
    void ShowScorePopup()
    {
        if (scorePopup != null)
        {
            scorePopup.Show(currentScore); // Muestra el puntaje total
        }
        else
        {
            Debug.LogError("scorePopup no está asignado en el Inspector");
        }
    }
}
