using UnityEngine;

public class Book : MonoBehaviour
{
    public string question = "¿Cuánto es 2 + 2?";
    public string[] options = { "5", "7", "4", "3" };
    public int correctOptionIndex = 2; // Índice de la opción correcta (c.4)
    public string bookColor;
    public string clue;
    private void OnTriggerEnter(Collider other)
    {
        // Asegúrate de que el objeto que entra tiene el componente PlayerController2
        if (other.CompareTag("personaje"))
        {
            PlayerController3 playerController = other.GetComponent<PlayerController3>();
            if (playerController != null)
            {
                playerController.ShowQuestion(this); // Llama a ShowQuestion con el libro actual
            }
        }
    }
}
