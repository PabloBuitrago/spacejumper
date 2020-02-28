using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public int puntosGanados = 10;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "IncrementarPuntos", puntosGanados);
            NotificationCenter.DefaultCenter().PostNotification(this, "Estadisticas", 2);
            NotificationCenter.DefaultCenter().PostNotification(this, "GanarBalas");
            Destroy(gameObject);
        }
    }
}
