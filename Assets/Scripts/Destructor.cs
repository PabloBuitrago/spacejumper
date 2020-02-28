using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Destructor : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeHaMuerto");
        }
        else
        {
            if (other.tag != "Boss" && other.tag != "BossDos")
            {
                Destroy(other.gameObject);
            }    
        }        
    }
}
