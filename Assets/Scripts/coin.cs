using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Capsule"))  // Asegúrate de que la cápsula tenga la etiqueta "Player"
        {
            Destroy(this.gameObject);
        }
    }
}
