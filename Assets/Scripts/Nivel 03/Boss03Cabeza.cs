using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss03Cabeza : MonoBehaviour {

    public float velocidad = 5f;
    public int puntosGanados = 50;

    GameObject camaraMain;
    Rigidbody2D rig;
    Animator anim;

    int salud = 2;
    int vidas;
    bool entrar = true;

    GameObject generadorPrincipal;
    GeneradorBoss generadorBoss;

    Text vidasBoss;

    GameObject bossDos;
    Boss03Cola boss03Cola;

    GameObject generadorDos;
    GeneradorSecundario generadorSecundario;

    Collider2D coll;

    public GameObject dis;

    GameObject saludBoss;
    Slider slider;
    Image[] imagenes = new Image[2];

    AudioSource audio_dis;

    void Awake()
    {
        audio_dis = GetComponent<AudioSource>();

        camaraMain = GameObject.FindGameObjectWithTag("MainCamera");
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        generadorPrincipal = GameObject.FindGameObjectWithTag("GeneradorPrincipal");
        generadorBoss = generadorPrincipal.GetComponent<GeneradorBoss>();

        saludBoss = GameObject.FindGameObjectWithTag("SaludBoss");
        slider = saludBoss.GetComponent<Slider>();
        imagenes = saludBoss.GetComponentsInChildren<Image>();

        //Inicializo el texto con la vida que tiene el Boss.
        vidasBoss = generadorBoss.vidasBoss;

        generadorDos = GameObject.FindGameObjectWithTag("GeneradorSecundario");
        generadorSecundario = generadorDos.GetComponent<GeneradorSecundario>();
    }

    void Start()
    {
        coll = GetComponent<Collider2D>();

        //Hago hijo al Gameobject que lleva este Script de la MainCamara.
        transform.SetParent(camaraMain.transform);
        //Le aplico una velocidad inicial.
        rig.velocity = new Vector2(-velocidad, rig.velocity.y);

        //Guardo en una variable la vida del Boss, que tiene en el Script GeneradorBoss.
        vidas = generadorBoss.vidas;

        //Genero la segunda parte del Boss.
        if (vidas == 1)
        {
            generadorSecundario.Generar();
        }
        else
        {
            generadorSecundario.Generar02();
        }

        //Activo el Slider y lo configuro.
        slider.maxValue = salud;
        slider.value = salud;

        for (int i = 0; i < 2; i++)
        {
            imagenes[i].enabled = true;
        }
    }

    void Update()
    {
        if (entrar)
        {
            //Calculo la posicion relativa del boss con la camara.
            float posicion = camaraMain.transform.position.x - transform.position.x;

            if (posicion > -8)
            {
                //Le aplico una velocidad para que vaya al otro lado.
                rig.velocity = new Vector2(1, 0.6f);
            }
            if (posicion < -10)
            {
                //Le aplico una velocidad para que vaya al otro lado.
                rig.velocity = new Vector2(-1, -0.6f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeHaMuerto");
        }

        if (other.gameObject.tag == "Bala")
        {
            //Le bajo uno la salud
            DecrementarSalud();

            //Inicio la variable del animator y hago la animacion del disparo (para que se vaya).
            anim.SetTrigger("disparo");

            //Si salud es cero quiere decir que ha muerto.
            if (salud == 0)
            {
                DecrementarVida();

                NotificationCenter.DefaultCenter().PostNotification(this, "IncrementarPuntos", puntosGanados);
                NotificationCenter.DefaultCenter().PostNotification(this, "Estadisticas", 1);

                //Si tiene una vida antes de decrementarle una vida (su vida es 0).
                if (vidas == 0)
                {
                    Destroy(coll);
                    Destroy(dis);

                    audio_dis.Play();

                    //Inicio la variable del Animator morir, para que el Boss explote.
                    anim.SetTrigger("morir");

                    //Inicio la variable del Animator morir, para que la parte dos explote.
                    bossDos = GameObject.FindGameObjectWithTag("BossDos");
                    Destroy(bossDos);
                    
                }
                else
                {
                    //Hago que desaparezca la parte dos del Boss.
                    bossDos = GameObject.FindGameObjectWithTag("BossDos");
                    boss03Cola = bossDos.GetComponent<Boss03Cola>();
                    boss03Cola.Desaparecer();

                    //Si tiene mas de una vida no dejo que se mueva.
                    entrar = false;
                    rig.velocity = new Vector2(5, 0);

                    //Invoco la funcion Morir a los dos segundos (dejandole tiempo para que se vaya).
                    Invoke("Morir", 1);
                }
            }
        }
    }

    public void Morir()
    {
        //Esta funcion la llamo desde un evento en el Animator.
        NotificationCenter.DefaultCenter().PostNotification(this, "ResetearBoss");

        Destroy(gameObject);
    }

    public void DecrementarVida()
    {
        //Desactivo el slider;
        for (int i = 0; i < 2; i++)
        {
            imagenes[i].enabled = false;
        }

        vidas--;
        //Actualizo la variable al Script GeneradorBoss.
        generadorBoss.vidas = vidas;

        //Cambio el texto de la vida del Boss.
        vidasBoss.text = "3 - " + vidas.ToString();
    }

    public void DecrementarSalud()
    {
        salud--;

        slider.value = salud;
    }

    void Completado()
    {
        //Esta funcion la llamo desde un evento en el Animator.
        NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeHaGanado");
        Morir();
    }
}
