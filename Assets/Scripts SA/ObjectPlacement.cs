using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    private int objectCount = 0; // Para contar los objetos colocados
    public string[] requiredObjectNames = { "ob1", "ob2", "ob3" }; // Nombres específicos de los objetos
    public string nextSceneName; // Nombre de la siguiente escena a cargar o evento a activar
    public CountdownTimer countdownTimer; // Referencia al temporizador

    private HashSet<string> objectsInPlace = new HashSet<string>(); // Almacena los nombres de los objetos colocados

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene el tag `PickupObject` y es uno de los nombres requeridos
        if (other.CompareTag("PickupObject") && IsRequiredObject(other.gameObject.name))
        {
            if (!objectsInPlace.Contains(other.gameObject.name)) // Evita contar el mismo objeto más de una vez
            {
                objectsInPlace.Add(other.gameObject.name);
                objectCount++;
                Debug.Log("Objeto detectado en la mesa: " + other.gameObject.name + ". Conteo actual: " + objectCount);
            }

            // Verifica si los tres objetos requeridos están colocados
            if (objectCount == requiredObjectNames.Length)
            {
                Debug.Log("Todos los objetos requeridos están en la mesa. Cambiando de escena...");

                // Desactivar el temporizador si existe
                if (countdownTimer != null)
                {
                    countdownTimer.enabled = false;
                }

                // Cambiar de escena
                UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    // Método para verificar si el objeto es uno de los requeridos
    bool IsRequiredObject(string objectName)
    {
        foreach (string requiredName in requiredObjectNames)
        {
            if (objectName == requiredName)
            {
                return true;
            }
        }
        return false;
    }
}
