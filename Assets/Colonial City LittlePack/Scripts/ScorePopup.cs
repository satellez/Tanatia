using UnityEngine;
using TMPro; // Si estás usando TextMeshPro

public class ScorePopup : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Asegúrate de arrastrar tu objeto de texto aquí

    // Método para mostrar o actualizar el puntaje total
    public void Show(int totalScore)
    {
        scoreText.text = "Score: " + totalScore.ToString(); // Muestra el puntaje total acumulado
        Canvas.ForceUpdateCanvases();
        gameObject.SetActive(true); // Asegúrate de que el objeto de texto esté activo
        Debug.Log("Popup activado: " + scoreText.text); // Añade esta línea
    }
}
