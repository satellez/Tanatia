using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController3 : MonoBehaviour
{
    [Header("UI References")]
    public GameObject questionPanel;
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;

    private int score = 0;
    private int scoreToWin = 5;

    [Header("Player Movement")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 5f;
    public CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    [Header("Mouse Look")]
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    public Transform cameraTransform;

    private void Start()
    {
        questionPanel.SetActive(false);
        messagePanel.SetActive(false);
        LockCursor();
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Player Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void ShowQuestion(Book book)
    {
        questionPanel.SetActive(true);
        questionText.text = book.question;
        UnlockCursor();

        // Set up answer buttons
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = book.options[i];
            int index = i; // Necessary to avoid closure issue
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => CheckAnswer(index, book));
        }
    }

    private void CheckAnswer(int selectedOption, Book book)
    {
        if (selectedOption == book.correctOptionIndex)
        {
            ShowNextClue(book); // Show clue for the next book if correct
            book.gameObject.SetActive(false); // Hide the book after answering
        }
        else
        {
            GameOver("Respuesta incorrecta. Nunca conoceras mi historia!!!!!!!.");
        }

        questionPanel.SetActive(false);
        LockCursor();
    }

    public void ShowNextClue(Book book)
    {
        ShowMessage(book.clue); // Show the next book's clue
        if (book.bookColor == "rojo")
        {
            // If it's the final book, load the next scene
            Invoke("LoadNextScene", 3f);
        }
        else
        {
            Invoke("HideMessage", 5f); // Hide message after 3 seconds for non-final books
        }
    }

    private void ShowMessage(string message, float displayDuration = 2f)
    {
        messagePanel.SetActive(true);
        messageText.text = message;
        Invoke("HideMessage", displayDuration); // Hide message after specified duration
    }

    private void HideMessage()
    {
        messagePanel.SetActive(false);
    }

    private void GameOver(string message)
    {
        ShowMessage(message);
        Invoke("RestartGame", 2f);
    }

    public void LoadNextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Cementerio");
        enabled = false;
    }

    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
