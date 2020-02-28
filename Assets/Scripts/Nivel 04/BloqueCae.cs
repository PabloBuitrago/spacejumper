using UnityEngine;
using System.Collections;

public class BloqueCae : MonoBehaviour {

    private bool haColisionado = false;

    public int puntosGanados = 2;

    private Rigidbody2D rig;
    private Collider2D coll;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Llama a la funcion IncrementarPuntos, para que incremente 1 punto y siempre que haya un Gameobject con el tag Boss.
        if (!haColisionado && other.gameObject.tag == "Finish")
        {
            if(GameObject.FindGameObjectWithTag("Boss") == false)
            {
                haColisionado = true;

                NotificationCenter.DefaultCenter().PostNotification(this, "IncrementarPuntos", puntosGanados);
                NotificationCenter.DefaultCenter().PostNotification(this, "Estadisticas", 3);
            }
            
            rig.isKinematic = false;

            Invoke("Parar", 0.2f);
        }

    }

    void Parar()
    {
        coll.isTrigger = true; 
    }
}
