using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public Transform holdArea;  // Lugar donde el objeto será sostenido
    private GameObject heldObject; // El objeto que estás agarrando
    private Rigidbody heldObjectRb; // Para guardar el Rigidbody del objeto


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Detecta cuando se presiona la tecla "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Si no estás sosteniendo ningún objeto actualmente
            if (heldObject == null)
            {
                // Crea un Ray que sale del centro de la pantalla (de las coordenadas de la cámara)
                // Screen.width / 2 y Screen.height / 2 aseguran que el Ray salga justo del centro
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit;

                // Lanza el Ray y verifica si golpea algo a una distancia máxima de 3 metros
                if (Physics.Raycast(ray, out hit, 3f))
                {
                    // Si el objeto golpeado tiene un Rigidbody, significa que es interactivo
                    if (hit.collider.gameObject.GetComponent<Rigidbody>())
                    {
                        // Llama a la función PickUp para agarrar el objeto
                        PickUp(hit.collider.gameObject);
                    }
                }
            }
            else
            {
                // Si ya estás sosteniendo un objeto, suéltalo al presionar "E" de nuevo
                DropObject();
            }
        }
    }

    void PickUp(GameObject pickObj)
    {
        heldObjectRb = pickObj.GetComponent<Rigidbody>();
        heldObjectRb.useGravity = false;
        heldObjectRb.drag = 10;  // Hace que el objeto no se mueva demasiado cuando lo agarras
        heldObjectRb.transform.position = holdArea.position; // Mueve el objeto a la posición de la mano
        heldObjectRb.transform.parent = holdArea; // Hace que el objeto siga la mano
        heldObject = pickObj;
    }

    void DropObject()
    {
        heldObjectRb.useGravity = true;
        heldObjectRb.drag = 1; // Vuelve a la configuración normal del objeto
        heldObjectRb.transform.parent = null; // El objeto ya no sigue la mano
        heldObject = null;
    }
}
