using UnityEngine;
using System.Collections;

public class Alien_Lider : MonoBehaviour {

    public Transform comprobarLadoD;
    public Transform comprobarLadoI;

    float comprobadorRadio = 0.07f;
    public LayerMask mascaraSuelo;

    public float velocidad = 4;

    bool enLadoDerecho;
    bool enLadoIzquierdo;

    public int puntosGanados = 15;

    Animator anim;
    Collider2D coll;
    Rigidbody2D rig;

    void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rig = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //Le aplico una velocidad inicial
        rig.velocity = new Vector2(-velocidad, rig.velocity.y);
    }
	
	void Update () {
        //Compruebo cuando llega al limite del bloque por la Derecha e Izquierda.
        enLadoDerecho = Physics2D.OverlapCircle(comprobarLadoD.position, comprobadorRadio, mascaraSuelo);
        enLadoIzquierdo = Physics2D.OverlapCircle(comprobarLadoI.position, comprobadorRadio, mascaraSuelo);
        //Si llego al lado Derecho cambio la velocidad.
        if (!enLadoIzquierdo)
        {
            rig.velocity = new Vector2(velocidad, rig.velocity.y);    
        }
        if (!enLadoDerecho)
        {
            rig.velocity = new Vector2(-velocidad, rig.velocity.y);
        }

        anim.SetFloat("velocidad", rig.velocity.x);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeDanado");
                break;
            case "Enemy":
                Destroy(gameObject);
                break;
            case "Bala":
                rig.simulated = false;
                Destroy(coll);

                NotificationCenter.DefaultCenter().PostNotification(this, "IncrementarPuntos", puntosGanados);
                NotificationCenter.DefaultCenter().PostNotification(this, "Estadisticas", 0);

                //Inicio la variable morir del Animator.
                anim.SetTrigger("morir");
                break;
        }
    }

    //(esta funcion la llamo desde el Animator con un evento en la animacion).
    void Muerte()
    {
        Destroy(gameObject);
    }
}
