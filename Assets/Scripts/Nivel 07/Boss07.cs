using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss07 : MonoBehaviour
{

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

    Collider2D coll;

    public GameObject dis;

    public GameObject escudo;

    bool bloquear = true;

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

        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeHaMuerto");

        //Hago hijo al Gameobject que lleva este Script de la MainCamara.
        transform.SetParent(camaraMain.transform);
        //Le aplico una velocidad inicial.
        rig.velocity = new Vector2(-velocidad, rig.velocity.y);

        //Guardo en una variable la vida del Boss, que tiene en el Script GeneradorBoss.
        vidas = generadorBoss.vidas;

        //Invoco la funcion Generar pasado 4 segundos (para que empiece a generar el escudo cuando se vea por pantalla).
        Invoke("Generar", 4);

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

            if (posicion > -6)
            {
                //Le aplico una velocidad para que vaya al otro lado.
                rig.velocity = new Vector2(1, 0.1f);
            }
            if (posicion < -7)
            {
                //Le aplico una velocidad para que vaya al otro lado.
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
                }
                else
                {
                    //Si tiene mas de una vida no dejo que se mueva.
                    entrar = false;
                    rig.velocity = new Vector2(10, 0);

                    //Invoco la funcion Morir al segundo (dejandole tiempo para que se vaya).
                    Invoke("Morir", 1);
                }
            }
        }
    }

    void Generar()
    {
        escudo.SetActive(false);

        if (bloquear)
        {
            //Si hay un 1 de los tres numeros aleatorios entra (un 33% de probabilidad).
            if (Random.Range(0, 2) == 1)
            {
                escudo.SetActive(true);

                //Invoco la funcion Parar pasado 2 segundos.
                Invoke("Parar", 2);
            }
            else
            {
                //Vuelvo a invocar la funcion Generar pasado 4 segundos.
                Invoke("Generar", 4);
            }   
        }
    }

    void Parar()
    {
        escudo.SetActive(false);

        //Vuelvo a invocar la funcion Generar pasado 2 segundos.
        Invoke("Generar", 2);
    }

    void PersonajeHaMuerto()
    {
        bloquear = false;
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
        vidasBoss.text = "7 - " + vidas.ToString();
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
