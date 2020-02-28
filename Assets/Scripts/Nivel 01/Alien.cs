using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {

    public int puntosGanados = 10;

    Animator anim;
    Collider2D coll;
    Rigidbody2D rig;

    void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rig = GetComponent<Rigidbody2D>();
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
