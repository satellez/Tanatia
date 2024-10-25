using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    private int objectCount = 0; // Para contar los objetos colocados
    public int requiredObjects = 3; // Necesitas colocar 3 objetos
    public string Cementerio; // Nombre de la siguiente escena a cargar

    private APIManager apiManager; // Referencia a APIManager

    void Start()
    {
        // Encuentra el componente APIManager en la escena
        apiManager = FindObjectOfType<APIManager>();

        // Verifica si el APIManager se encontró correctamente
        if (apiManager == null)
        {
            Debug.LogError("No se encontró el componente APIManager en la escena.");
        }
        else
        {
            Debug.Log("APIManager encontrado con éxito.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickupObject"))
        {
            Debug.Log("Objeto detectado: " + other.gameObject.name); // Para verificar si el objeto entra
            objectCount++;
            Debug.Log("Objetos colocados en la mesa: " + objectCount); // Para ver cuántos objetos se han colocado

            other.gameObject.SetActive(false); // Desactiva el objeto para simular que está en la mesa

            if (objectCount >= requiredObjects)
            {
                Debug.Log("Se han colocado suficientes objetos. Preparándose para cambiar de escena.");

                if (!string.IsNullOrEmpty(Cementerio))
                {
                    // Verificar que APIManager esté configurado antes de llamar a SendBookData
                    if (apiManager != null)
                    {
                        // Enviar los datos de la escena a la API antes de cambiar de escena
                        Debug.Log("Enviando datos a la API...");
                        apiManager.SendBookData(Cementerio);
                    }
                    else
                    {
                        Debug.LogError("APIManager no está asignado. No se puede enviar los datos.");
                    }

                    // Cambia la escena después de enviar los datos
                    Debug.Log("Cambiando a la escena: " + Cementerio);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(Cementerio);
                }
                else
                {
                    Debug.LogError("El nombre de la escena no está asignado o es incorrecto.");
                }
            }
        }
    }
}
