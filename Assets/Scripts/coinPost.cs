using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class coinPost : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Character")
        {
            StartCoroutine(SendPostRequest());
        }
    }

    IEnumerator SendPostRequest()
    {
        // Crear el objeto con los datos de "Books"
        Book bookData = new Book
        {
            id = 0,
            idEdition = 0,
            title = "string",
            code = "string",
            publicationYear = "2024-10-25",
            edition = new Edition
            {
                id = 0,
                editionName = "string",
                isDelete = true
            },
            isDelete = true
        };

        // Serializar el objeto a JSON
        string jsonData = JsonUtility.ToJson(bookData);

        // Crear la solicitud POST
        UnityWebRequest www = new UnityWebRequest("https://www.bibliotecasantotomas.somee.com/api/Books", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        // Enviar la solicitud
        yield return www.SendWebRequest();

        // Verifica si hubo algún error en la conexión
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);  // Muestra el error en la consola
        }
        else
        {
            // Mostrar resultados como texto
            Debug.Log(www.downloadHandler.text);
        }

        // Destruir el objeto de la moneda después de la solicitud
        Destroy(this.gameObject);

        // Liberar recursos de UnityWebRequest
        www.Dispose();
    }
}

// Clases para el modelo Book y Edition
[System.Serializable]
public class Book
{
    public int id;
    public int idEdition;
    public string title;
    public string code;
    public string publicationYear;
    public Edition edition;
    public bool isDelete;
}

[System.Serializable]
public class Edition
{
    public int id;
    public string editionName;
    public bool isDelete;
}
