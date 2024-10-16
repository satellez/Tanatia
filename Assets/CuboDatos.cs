using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarraObjetos : MonoBehaviour
{
    public GameObject cubo;
    public Transform mano;

    private bool activo;

    void Update()
    {
        if (activo == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Agarrar el objeto
                cubo.transform.SetParent(mano);
                cubo.transform.position = mano.position;
                Rigidbody rb = cubo.GetComponent<Rigidbody>();
                rb.isKinematic = true;  // Desactiva la f�sica
                rb.velocity = Vector3.zero;  // Reinicia la velocidad
                rb.angularVelocity = Vector3.zero;  // Reinicia la rotaci�n
                rb.useGravity = false;  // Desactiva la gravedad
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            // Soltar el objeto
            cubo.transform.SetParent(null);
            Rigidbody rb = cubo.GetComponent<Rigidbody>();
            rb.isKinematic = false;  // Activa la f�sica
            rb.velocity = Vector3.zero;  // Aseg�rate de que no haya velocidad residual
            rb.angularVelocity = Vector3.zero;  // Aseg�rate de que no haya rotaci�n residual
            rb.useGravity = true;  // Activa la gravedad
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            activo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            activo = false;
        }
    }
}

