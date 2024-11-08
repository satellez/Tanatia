using UnityEngine;

public class SceneSpecificGravity : MonoBehaviour
{
    private Vector3 defaultGravity;

    void Start()
    {
        // Guarda la gravedad predeterminada
        defaultGravity = Physics.gravity;

        // Ajusta la gravedad solo para esta escena
        Physics.gravity = new Vector3(0, -200f, 0); // Cambia -20f según lo que necesites
    }

    void OnDisable()
    {
        // Restaura la gravedad cuando sales de la escena
        Physics.gravity = defaultGravity;
    }
}
