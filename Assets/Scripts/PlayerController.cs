using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;

    public float playerSpeed;
    public float jumpForce;
    public float crouchSpeed = 2f;  // Velocidad mientras se agacha
    public float normalHeight = 2f;  // Altura normal del jugador
    public float crouchHeight = 1f;  // Altura cuando se agacha

    public bool onGround;
    public bool crouch;

    CapsuleCollider playerCollider;  // Para ajustar la altura del jugador

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();  // Obtenemos el CapsuleCollider
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        playerRb.MovePosition(transform.position + movement * playerSpeed * Time.deltaTime);

        // Saltar
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
        }

        // Crouch
        crouch = Input.GetKey(KeyCode.LeftControl);

        if (crouch)
        {
            // Cambiamos la altura del CapsuleCollider para simular que el jugador se agacha
            playerCollider.height = crouchHeight;
            playerSpeed = crouchSpeed;  // Disminuimos la velocidad mientras está agachado
        }
        else
        {
            // Restauramos la altura normal cuando se deja de agachar
            playerCollider.height = normalHeight;
            playerSpeed = 5f;  // Velocidad normal al caminar
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            onGround = true;
        }
    }
}
