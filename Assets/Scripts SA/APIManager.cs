using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class APIManager : MonoBehaviour
{
    // URL del endpoint de la API (cambia esta URL por la de tu API)
    private string apiUrl = "https://www.bibliotecasantotomas.somee.com/api/Books";

    // Método para enviar los datos cuando se cambia de escena
    public void SendBookData(string sceneName)
    {
        StartCoroutine(PostData(sceneName));
    }

    // Corrutina que realiza la solicitud POST
    IEnumerator PostData(string sceneName)
    {
        // Crea el objeto con los datos en el formato que espera la API
        BookData data = new BookData
        {
            id = 0, // El ID se establecerá en la base de datos o si lo gestionas manualmente
            idEdition = 1, // El idEdition debe coincidir con lo que espera tu API
            title = "CheckPointCementerio", // Cambia este valor por el título que desees
            code = "77777", // Código del libro, cámbialo por el código que la API espera
            publicationYear = "2024-10-25", // Fecha de publicación fija como en el ejemplo
            edition = new Edition
            {
                id = 0, // ID de la edición
                editionName = "string", // Puedes cambiar esto a algo relevante
                isDelete = false // Estado de eliminación, lo establecemos en false
            },
            isDelete = false // Estado de eliminación del libro en sí, lo establecemos en false
        };

        // Convierte los datos a formato JSON
        string jsonData = JsonUtility.ToJson(data);
        Debug.Log("Datos JSON a enviar: " + jsonData);

        // Crea la solicitud POST
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Envía la solicitud
        yield return request.SendWebRequest();

        // Maneja la respuesta
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error al enviar datos: " + request.error);
        }
        else
        {
            Debug.Log("Datos enviados exitosamente. Respuesta del servidor: " + request.downloadHandler.text);
        }
    }

    // Clase para los datos del POST
    [Serializable]
    public class BookData
    {
        public int id; // ID del libro
        public int idEdition; // ID de la edición
        public string title; // Título del libro o cambio de escena
        public string code; // Código del libro o evento
        public string publicationYear; // Año de publicación
        public Edition edition; // Datos de la edición
        public bool isDelete; // Estado de eliminación
    }

    // Clase para los detalles de la edición
    [Serializable]
    public class Edition
    {
        public int id; // ID de la edición
        public string editionName; // Nombre de la edición
        public bool isDelete; // Estado de eliminación para la edición
    }
}
