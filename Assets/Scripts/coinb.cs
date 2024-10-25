using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class coinb : MonoBehaviour
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
            StartCoroutine(SendRequest());
        }
    }

    IEnumerator SendRequest()
    {
        // Cambia la URL a HTTPS para evitar conexiones inseguras
        UnityWebRequest www = UnityWebRequest.Get("https://www.bibliotecasantotomas.somee.com/api/Books");
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

            // O puedes obtener los resultados como datos binarios si es necesario
            byte[] results = www.downloadHandler.data;
        }

        // Destruir el objeto de la moneda después de la solicitud
        Destroy(this.gameObject);

        // Liberar recursos de UnityWebRequest
        www.Dispose();
    }
}
