using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss00 : MonoBehaviour {

    public float velocidad = 5f;
    public int puntosGanados = 50;

    GameObject camaraMain;
    Rigidbody2D rig;
    Animator anim;

    int salud = 3;
    int vidas;
    bool entrar = true;

    GameObject generadorPrincipal;
    GeneradorBoss generadorBoss;

    Text vidasBoss;

    Collider2D coll;

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
                rig.velocity = new Vector2(1, 0.1f);
            }
            if (posicion < -10)
            {
                //Le aplico una velocidad negativa para que vaya al otro lado.
                rig.velocity = new Vector2(-1, -0.1f);
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

            //Inicio la variable del Animator y hago la animacion del da�o.
            anim.SetTrigger("disparo");

            //Si salud es cero quiere decir que ha muerto.
            if (salud == 0)
            {
                Destroy(coll);

                DecrementarVida();

                NotificationCenter.DefaultCenter().PostNotification(this, "IncrementarPuntos", puntosGanados);
                NotificationCenter.DefaultCenter().PostNotification(this, "Estadisticas", 1);

                audio_dis.Play();

                //Inicio la variable del Animator y hago la animacion de morir.
                anim.SetTrigger("morir");
            }
        }
    }

    public void Morir()
    {
        //Esta funcion la llamo desde un evento en el Animator.
        NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeHaGanado");

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
        vidasBoss.text = "0 - " + vidas.ToString();
    }

    public void DecrementarSalud()
    {
        salud--;

        slider.value = salud;
    }
}
