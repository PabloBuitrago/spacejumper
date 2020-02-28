using UnityEngine;
using System.Collections;

public class Bloque : MonoBehaviour {

    private bool haColisionado = false;

    public int puntosGanados = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Llama a la funcion IncrementarPuntos, para que incremente 1 punto y siempre que haya un Gameobject con el tag Boss.
        if (!haColisionado && other.gameObject.tag == "Finish" && GameObject.FindGameObjectWithTag("Boss") == false)
        {
            haColisionado = true;

            NotificationCenter.DefaultCenter().PostNotification(this, "IncrementarPuntos", puntosGanados);
            NotificationCenter.DefaultCenter().PostNotification(this, "Estadisticas", 3);
        }
        
    }
}
