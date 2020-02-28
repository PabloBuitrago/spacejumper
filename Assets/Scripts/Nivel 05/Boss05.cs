using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss05 : MonoBehaviour
{

    public float velocidad = 5f;
    public int puntosGanados = 50;

    GameObject camaraMain;
    Rigidbody2D rig;
    Animator anim;

    int salud = 5;
    int vidas;
    bool entrar = true;

    GameObject generadorPrincipal;
    GeneradorBoss generadorBoss;

    Text vidasBoss;

    public GameObject laser;
    public GameObject escudo;

    bool bloquear = false;

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

        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeHaMuerto");

        //Hago hijo al Gameobject que lleva este Script de la MainCamara.
        transform.SetParent(camaraMain.transform);
        //Le aplico una velocidad inicial.
        rig.velocity = new Vector2(-velocidad, 2);

        //Guardo en una variable la vida del Boss, que tiene en el Script GeneradorBoss.
        vidas = generadorBoss.vidas;

        //Invoco la funcion Generar pasado 8 segundos (para que empiece a disparar cuando se vea por pantalla).
        Invoke("Generar", 8);

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
            float posicionX = camaraMain.transform.position.x - transform.position.x;
            float posicionY = camaraMain.transform.position.y - transform.position.y;

            if (posicionX < -8)
            {
                //Le aplico una velocidad para que vaya al otro lado.
                rig.velocity = new Vector2(-1, rig.velocity.y);
            }
            if (posicionX > -6)
            {
                //Le aplico una velocidad para que vaya al otro lado.
                rig.velocity = new Vector2(1, rig.velocity.y);
            }            

            if (posicionY > 5)
            {
                //Le aplico una velocidad para que vaya hasta arriba.
                rig.velocity = new Vector2(rig.velocity.x, 3);
            }
            if (posicionY < -6)
            {
                //Le aplico una velocidad para que vaya hasta abajo.
                rig.velocity = new Vector2(rig.velocity.x, -3);
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

                Destroy(coll);
                bloquear = true;

                audio_dis.Play();

                //Inicio la variable del Animator morir, para que el Boss explote.
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
        vidasBoss.text = "5 - " + vidas.ToString();
    }

    public void DecrementarSalud()
    {
        salud--;

        slider.value = salud;
    }

    void PersonajeHaMuerto()
    {
        bloquear = true;
    }

    void Generar()
    {
        if (!bloquear)
        {
            //Si hay un 1 de los tres numeros aleatorios entra (un 33% de probabilidad).
            if (Random.Range(0, 2) == 1)
            {
                escudo.SetActive(true);
                anim.SetTrigger("parpadear");
            }

            //Vuelvo a invocar la funcion Generar pasado 8 segundos.
            Invoke("Generar", 8);
        }
    }

    public void Disparar()
    {
        //Desactivo el escudo, Activo el laser.
        escudo.SetActive(false);
        laser.SetActive(true);
    }
}

