using UnityEngine;
using System.Collections;

public class BloqueMueve : MonoBehaviour {

    private bool haColisionado = false;

    public int puntosGanados = 7;

    private Rigidbody2D rig;

    public float velocidad = 2;

    bool entrar = true;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        rig.velocity = new Vector2(velocidad, rig.velocity.y);
    }

    void Update()
    {
        if (entrar)
        {
            entrar = false;
            Invoke("Lado", 2);
        }
    }

    void Lado()
    {
        velocidad = -velocidad;
        rig.velocity = new Vector2(velocidad, rig.velocity.y);

        entrar = true;  
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Llama a la funcion IncrementarPuntos, para que incremente 1 punto y siempre que haya un Gameobject con el tag Boss.
        if (!haColisionado && other.gameObject.tag == "Finish")
        {
            if (GameObject.FindGameObjectWithTag("Boss") == false)
            {
                haColisionado = true;

                NotificationCenter.DefaultCenter().PostNotification(this, "IncrementarPuntos", puntosGanados);
                NotificationCenter.DefaultCenter().PostNotification(this, "Estadisticas", 3);
            }
        }

    }
}
