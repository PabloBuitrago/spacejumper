using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControladorPersonaje : MonoBehaviour {

	public float fuerzaSalto = 100f;

	Animator animator;
    Rigidbody2D rig;

    public bool enSuelo = true;
    public Transform comprobarSuelo;
    float comprobadorRadio = 0.07f;
    public LayerMask mascaraSuelo;

    public int dobleSalto = 0;

    bool corriendo = false;
    public float velocidad = 6f;

    public Pulsar pulsar;

    float timer;
    public float tiempoPulsaciones = 0.5f;

    public int saltos;

    public AudioSource audio_salto;

    public AudioSource audio_item;

    void Awake(){
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
        if (corriendo)
        {
            //Pongo velocidad al rig.
            rig.velocity = new Vector2(velocidad, rig.velocity.y);
        }
        //Pongo a la variable velocidad del Animator la velocidad que tiene el objeto.
        animator.SetFloat("velocidad", rig.velocity.x);

        //Compruebo con la mascaraSuelo y si el personaje esta tocando el Suelo.
        enSuelo = Physics2D.OverlapCircle(comprobarSuelo.position, comprobadorRadio, mascaraSuelo);

        //Paso al Animator si esta o no tocando el suelo (true o false).
        animator.SetBool("enSuelo", enSuelo);

        if (enSuelo)
        {
            dobleSalto = 0;
        }
    }
	
	void Update () {
        timer += Time.deltaTime;

        //Si se ha pulsado la pantalla o el boton Fire1 y timer es mayor o igual al tiempoPulsaciones.
        if (timer >= tiempoPulsaciones && (pulsar.pulsado || Input.GetButtonDown("Fire1")))
        {
            if (corriendo)
            {
                if (dobleSalto <= 1)
                {
                    audio_salto.Play();

                    saltos++;

                    //Mantengo la velocidad que lleva el rig y anado una velocidad de salto.
                    rig.velocity = new Vector2(rig.velocity.x, fuerzaSalto);
                    //Anado una fuerza al Gameobject.
                    rig.AddForce(new Vector2(0, fuerzaSalto));

                    dobleSalto++;

                    if (enSuelo)
                    {
                        Invoke("Esperar", 0.2f);
                    }
                }
            }
            else
            {
                corriendo = true;
                NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeEmpiezaACorrer");        
            }
            pulsar.pulsado = false;
            timer = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Item")
        {
            audio_item.Play();
        }
    }

    void Esperar()
    {
        dobleSalto = 1;
    }
}
