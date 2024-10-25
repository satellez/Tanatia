using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public Camera playerCamera; // Referencia a la cámara del jugador
    public float pickupRange = 5f; // Ajusta el rango de interacción
    public Transform holdPosition; // Punto donde se colocará el objeto recogido
    private GameObject pickedObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Presiona 'E' para recoger o soltar el objeto
        {
            if (pickedObject == null) // Si no hay un objeto recogido, intenta recogerlo
            {
                TryPickUpObject();
            }
            else // Si ya tienes un objeto recogido, suéltalo
            {
                DropObject();
            }
        }
    }

    void TryPickUpObject()
    {
        // Dispara el rayo desde el centro de la pantalla hacia adelante
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.transform != null && hit.transform.CompareTag("PickupObject"))
            {
                pickedObject = hit.transform.gameObject;

                // Hacer que el objeto recogido sea hijo del punto de sostén
                pickedObject.transform.SetParent(holdPosition);
                pickedObject.transform.localPosition = Vector3.zero; // Poner el objeto en la posición del HoldPosition
                pickedObject.transform.localRotation = Quaternion.identity; // Restablecer la rotación del objeto
                pickedObject.GetComponent<Rigidbody>().isKinematic = true; // Desactivar la física mientras lo sostienes
            }
            else
            {
                Debug.Log("El objeto no tiene el tag PickupObject o el ray no ha golpeado ningún objeto.");
            }
        }
        else
        {
            Debug.Log("El ray no ha golpeado ningún objeto.");
        }
    }

    void DropObject()
    {
        if (pickedObject != null)
        {
            // Soltar el objeto
            pickedObject.transform.SetParent(null); // Desvincular el objeto del jugador
            pickedObject.GetComponent<Rigidbody>().isKinematic = false; // Reactivar la física
            pickedObject = null; // Ya no estamos sosteniendo ningún objeto
        }
    }
}
