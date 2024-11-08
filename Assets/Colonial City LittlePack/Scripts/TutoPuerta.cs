using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoPuerta : MonoBehaviour
{
    public float interactionDistance = 3f; // Distancia a la cual se puede interactuar con la puerta
    public float openAngle = 90f;          // Ángulo de apertura
    public float openSpeed = 2f;           // Velocidad de apertura/cierre

    private bool isOpen = false;           // Estado de la puerta (abierta/cerrada)
    private bool isInRange = false;        // Si el jugador está en el rango de interacción
    private Quaternion closedRotation;     // Rotación inicial de la puerta
    private Quaternion openRotation;       // Rotación final (abierta) de la puerta
    private Transform player;              // Referencia al objeto del jugador

    void Start()
    {
        // Encuentra al jugador por su tag "personaje"
        GameObject playerObject = GameObject.FindGameObjectWithTag("personaje");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation; // Rotación cuando la puerta está abierta
    }

    void Update()
    {
        if (player != null)
        {
            CheckPlayerDistance();

            // Abre o cierra la puerta cuando el jugador está cerca y hace clic derecho
            if (isInRange && Input.GetMouseButtonDown(1))
            {
                ToggleDoor();
            }

            RotateDoor();
        }
    }

    void CheckPlayerDistance()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        isInRange = distance <= interactionDistance;
    }

    void ToggleDoor()
    {
        isOpen = !isOpen; // Cambia el estado de la puerta
    }

    void RotateDoor()
    {
        // Lerp entre las rotaciones cerrada y abierta en función del estado actual
        transform.rotation = Quaternion.Slerp(transform.rotation, isOpen ? openRotation : closedRotation, Time.deltaTime * openSpeed);
    }
}