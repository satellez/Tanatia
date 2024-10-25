using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class LoginManager : MonoBehaviour
{
    public InputField emailField;  // Cambiar a InputField para email
    public InputField passwordField;
    public Text errorMessage;
    public Button loginButton;

    private string loginUrl = "https://bibliotecasantotomas.somee.com/api/User/login"; // Cambia esto a la URL de tu API

    void Start()
    {
        loginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    void OnLoginButtonClicked()
    {
        string email = emailField.text;  // Cambiar a email
        string password = passwordField.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            errorMessage.text = "Por favor ingrese su email y contraseña.";
            return;
        }

        StartCoroutine(Login(email, password));
    }

    IEnumerator Login(string email, string password)
    {
        // Crear la data del JSON para enviar
        var loginData = new LoginData { Email = email, Password = password };  // Cambiar Username a Email
        string jsonData = JsonUtility.ToJson(loginData);

        Debug.Log("Datos enviados: " + jsonData);  // Agregar depuración

        using (UnityWebRequest www = new UnityWebRequest(loginUrl, "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            www.uploadHandler.contentType = "application/json";
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            Debug.Log("Estado de la solicitud: " + www.result);
            Debug.Log("Código de respuesta: " + www.responseCode);

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error en la conexión: " + www.error);
                errorMessage.text = "Error de conexión: " + www.error;
            }
            else
            {
                Debug.Log("Respuesta recibida: " + www.downloadHandler.text);

                if (www.responseCode == 200)
                {
                    Debug.Log("Inicio de sesión exitoso.");
                    errorMessage.text = "Inicio de sesión exitoso.";
                    SceneManager.LoadScene("Cementerio"); // Cambiar a la escena principal
                }
                else if (www.responseCode == 401)
                {
                    errorMessage.text = "Usuario o contraseña incorrectos.";
                }
                else
                {
                    errorMessage.text = "Error de inicio de sesión: " + www.downloadHandler.text;
                }
            }
        }
    }

    // Clase para representar los datos de inicio de sesión
    [System.Serializable]
    public class LoginData
    {
        public string Email;    // Cambiar Username a Email
        public string Password;
    }
}
