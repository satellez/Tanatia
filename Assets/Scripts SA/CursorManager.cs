using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Camera playerCamera; // Asigna tu cámara de jugador
    public float interactRange = 5f; // Rango para interactuar con objetos
    public LayerMask interactableLayer; // Capa de objetos interactuables

    void Start()
    {
        // Bloquea el cursor y lo oculta
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Presiona 'E' para interactuar
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        // Raycast desde el centro de la cámara
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
        {
            Debug.Log("Interactuaste con: " + hit.collider.gameObject.name);
            // Puedes añadir lógica para recoger el objeto aquí
            hit.collider.gameObject.SetActive(false); // Ejemplo: desactiva el objeto
        }
        else
        {
            Debug.Log("No hay objetos interactuables en el rango.");
        }
    }
}
