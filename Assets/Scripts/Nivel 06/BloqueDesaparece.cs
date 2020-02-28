using UnityEngine;
using System.Collections;

public class BloqueDesaparece : MonoBehaviour {

    private bool haColisionado = false;

    public int puntosGanados = 5;

    private Collider2D coll;
    private Animator anim;

    private AudioSource audio_dis;

    void Start()
    {
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        audio_dis = GetComponent<AudioSource>();
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

            Invoke("Destruir", 0.2f);
        }
    }

    void Destruir()
    {
        audio_dis.Play();

        Destroy(coll);

        //Inicio la variable morir del Animator.
        anim.SetTrigger("morir");
    }

    //(esta funcion la llamo desde el Animator con un evento en la animacion).
    void Muerte()
    {
        Destroy(gameObject);
    }
}
