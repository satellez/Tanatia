using UnityEngine;

public class Basurero : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene el tag "Destruible"
        if (other.CompareTag("Destruible"))
        {
            Destroy(other.gameObject); // Destruye el objeto
            // Aquí puedes agregar efectos de sonido o animaciones si lo deseas
        }
    }
}
