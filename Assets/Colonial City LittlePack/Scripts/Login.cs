using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class LoginManager : MonoBehaviour
{
    public InputField usernameField;
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
        string username = usernameField.text;
        string password = passwordField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            errorMessage.text = "Por favor ingrese su usuario y contrase�a.";
            return;
        }

        StartCoroutine(Login(username, password));
    }

    IEnumerator Login(string username, string password)
    {
        // Crear la data del JSON para enviar
        var loginData = new LoginData { Username = username, Password = password };
        string jsonData = JsonUtility.ToJson(loginData);

        // Configurar el request
        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(loginUrl, jsonData))
        {
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            www.uploadHandler.contentType = "application/json";

            www.downloadHandler = new DownloadHandlerBuffer();

            // Enviar el request y esperar la respuesta
            yield return www.SendWebRequest();

            // Verificar el resultado de la solicitud
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error en la conexión: " + www.error); // Mostrar error en la consola
                errorMessage.text = "Error de conexión: " + www.error; // Mostrar error en pantalla
            }
            else
            {
                Debug.Log("Respuesta recibida: " + www.downloadHandler.text); // Mostrar respuesta en la consola

                // Aquí puedes verificar si la API envía un mensaje de éxito o un token
                if (www.responseCode == 200) // Asegúrate de que el código de éxito es 200 (HTTP OK)
                {
                    Debug.Log("Inicio de sesión exitoso.");
                    errorMessage.text = "Inicio de sesión exitoso."; // Mensaje en pantalla

                    // Cargar la siguiente escena
                    SceneManager.LoadScene("MainGameScene"); // Cambiar a la escena principal
                }
                else
                {
                    // Si la respuesta no es la esperada, muestra un mensaje de error
                    errorMessage.text = "Error de inicio de sesión: " + www.downloadHandler.text;
                }
            }
        }
    }


    // Clase para representar los datos de inicio de sesi�n
    [System.Serializable]
    public class LoginData
    {
        public string Username;
        public string Password;
    }
}
