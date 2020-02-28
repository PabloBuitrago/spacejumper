using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControladorPersonajeModel : MonoBehaviour
{
    public float fuerzaSalto = 100f;

    Animator animator;
    Rigidbody2D rig;

    bool enSuelo = true;
    public Transform comprobarSuelo;
    float comprobadorRadio = 0.07f;
    public LayerMask mascaraSuelo;

    bool dobleSalto = false;

    public Pulsar pulsar;

    float timer;
    public float tiempoPulsaciones = 0.5f;

    AudioSource audio_salto;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        audio_salto = GetComponent<AudioSource>();
    }

    //Se inicia antes del Update.
    void FixedUpdate()
    {
        //Compruebo con la mascaraSuelo y si el personaje esta tocando el Suelo.
        enSuelo = Physics2D.OverlapCircle(comprobarSuelo.position, comprobadorRadio, mascaraSuelo);

        //Paso al Animator si esta o no tocando el suelo (true o false).
        animator.SetBool("enSuelo", enSuelo);

        if (enSuelo)
        {
            dobleSalto = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        //Si se ha pulsado la pantalla o el boton Fire1 y timer es mayor o igual al tiempoPulsaciones.
        if (timer >= tiempoPulsaciones && (pulsar.pulsado || Input.GetButtonDown("Fire1")))
        {
            if (enSuelo || !dobleSalto)
            {
                audio_salto.Play();

                //Mantengo la velocidad que lleva el rig y anado una velocidad de salto.
                rig.velocity = new Vector2(rig.velocity.x, fuerzaSalto);
                //Anado una fuerza al Gameobject.
                rig.AddForce(new Vector2(0, fuerzaSalto));

                if (!dobleSalto && !enSuelo)
                {
                    dobleSalto = true;
                }
            }
            pulsar.pulsado = false;
            timer = 0f;
        }
    }
}

