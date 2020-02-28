using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Disparar : MonoBehaviour {

    public Animator anim;
    public Pulsar pulsar;

    float timer;
    public float tiempoPulsaciones = 0.5f;

    public GameObject obj;

    public int disparos;

    public Text balas;

    public int balasCont = 30;

    int balasMax;

    AudioSource audio_dis;

    void Start()
    {
        audio_dis = GetComponent<AudioSource>();

        balasMax = balasCont;

        NotificationCenter.DefaultCenter().AddObserver(this, "GanarBalas");
    }

    void Update () {
        timer += Time.deltaTime;

        //Si se ha pulsado la pantalla o el boton Fire2, puede entrar y timer es mayor o igual al tiempoPulsaciones.
        if (timer >= tiempoPulsaciones && (pulsar.pulsado || Input.GetButtonDown("Fire2")) && balasCont > 0)
        {
            audio_dis.Play();

            DescontarBalas();

            disparos++;

            //Inicio la variable trigger disparo, del animator.
            anim.SetTrigger("disparo");

            Disparo();
            
            timer = 0f;
        }

        pulsar.pulsado = false;
    }

    public void Disparo()
    {
        //Instancio el Gameobject Bala (definido como variable publica), en la posicion de este Gameobject.
        Instantiate(obj, transform.position, Quaternion.identity);
    }

    void DescontarBalas()
    {
        balasCont--;
        balas.text = balasCont.ToString();
    }

    void GanarBalas()
    {
        if (balasCont < balasMax)
        {
            balasCont++;
            balas.text = balasCont.ToString();
        }      
    }
}
