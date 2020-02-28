using UnityEngine;
using System.Collections;

public class Nave : MonoBehaviour {

    public GameObject completado;

    public GeneradorBoss generadorBoss;

    public bool recoger = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && generadorBoss.vidas == 0)
        {
            //El personaje se desactiva.
            other.gameObject.SetActive(false);

            //El Gameobject de completado se activa.
            completado.SetActive(true);

            //Ha recogido al personaje.
            recoger = true;
        }
    }

    void Completado()
    {
        NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeHaMuerto");
    }
}
