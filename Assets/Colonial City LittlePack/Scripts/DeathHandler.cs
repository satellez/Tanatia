using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Para reiniciar la escena

public class DeathHandler : MonoBehaviour
{
    public GameObject antagonist; // Referencia al antagonista (enemigo)
    public GameObject bloodScreen; // Referencia a la pantalla de sangre
    public Camera playerCamera; // Referencia a la cámara del jugador
    public PlayerController2 playerController; // Referencia al script de control del jugador

    public float deathDistance = 2.8f; // Distancia a la que el jugador muere
    private bool isDead = false; // Para evitar que la muerte se ejecute varias veces

    void Start()
    {
        // Si no tienes la referencia del antagonista, la conseguimos automáticamente
        if (antagonist == null)
            antagonist = GameObject.Find("Antagonist"); // Cambia "Antagonist" por el nombre del enemigo

        // Asegurarnos de que la pantalla de sangre esté desactivada al principio
        if (bloodScreen != null)
            bloodScreen.SetActive(false);
    }

    void Update()
    {
        // Si ya está muerto, no hacemos más cálculos
        if (isDead) return;

        // Calcular la distancia entre el jugador y el antagonista
        float distanceToAntagonist = Vector3.Distance(transform.position, antagonist.transform.position);

        // Si el antagonista está a una distancia menor o igual a deathDistance, activamos la muerte
        if (distanceToAntagonist <= deathDistance)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        // Mostrar la pantalla de sangre
        if (bloodScreen != null)
        {
            bloodScreen.SetActive(true); // Activa la pantalla de sangre
        }

        // Desactivar el movimiento del jugador (desactiva el script de movimiento)
        if (playerController != null)
        {
            playerController.enabled = false; // Desactiva el script de movimiento
        }

        // Desactivar la cámara
        if (playerCamera != null)
        {
            playerCamera.gameObject.SetActive(false); // Desactiva la cámara
        }

        // Espera 15 segundos y luego reinicia la escena
        StartCoroutine(DisablePlayerAfterDeath());
    }

    IEnumerator DisablePlayerAfterDeath()
    {
        // Espera 15 segundos antes de reiniciar la escena
        yield return new WaitForSeconds(15f);

        // Reiniciar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
