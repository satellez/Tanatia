using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Asegúrate de tener TextMeshPro para el mensaje

[RequireComponent(typeof(CharacterController))]
public class PlayerController4 : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public GameObject endGamePanel; // Panel de finalización del juego
    public TextMeshProUGUI endGameText; // Texto de finalización del juego

    [Header("General")]
    public float gravityScale = -20f;

    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    [Header("Rotation")]
    public float rotationSensibility = 100f;

    [Header("Jump")]
    public float jumpHeight = 1.9f;

    private float cameraVerticalAngle;
    Vector3 moveInput = Vector3.zero;
    Vector3 rotationinput = Vector3.zero;
    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        endGamePanel.SetActive(false); // Asegúrate de que el panel esté oculto al inicio
    }

    private void Update()
    {
        Look();
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el jugador ha chocado con el cuchillo
        if (other.CompareTag("Cuchillo"))
        {
            // Muestra el mensaje de finalización del juego
            ShowEndGameMessage("¡Felicitaciones, juego finalizado!");
        }
    }

    private void Move()
    {
        if (characterController.isGrounded)
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);

            if (Input.GetButton("Sprint"))
            {
                moveInput = transform.TransformDirection(moveInput) * runSpeed;
            }
            else
            {
                moveInput = transform.TransformDirection(moveInput) * walkSpeed;
            }

            if (Input.GetButtonDown("Jump"))
            {
                moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
            }
        }

        moveInput.y += gravityScale * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);
    }

    private void Look()
    {
        rotationinput.x = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
        rotationinput.y = Input.GetAxis("Mouse Y") * rotationSensibility * Time.deltaTime;

        cameraVerticalAngle = cameraVerticalAngle + rotationinput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);

        transform.Rotate(Vector3.up * rotationinput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0f, 0f);
    }

    private void ShowEndGameMessage(string message)
    {
        endGamePanel.SetActive(true);
        endGameText.text = message;
        Time.timeScale = 0f; // Pausa el juego
    }
}
