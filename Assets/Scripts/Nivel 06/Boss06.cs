using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss06 : MonoBehaviour
{

    public float velocidad = 5f;
    public int puntosGanados = 50;

    GameObject camaraMain;
    Rigidbody2D rig;
    Animator anim;

    int salud = 4;
    int vidas;
    bool entrar = true;

    GameObject generadorPrincipal;
    GeneradorBoss generadorBoss;

    Text vidasBoss;

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
    }

    void Start()
    {
        coll = GetComponent<Collider2D>();

        //Hago hijo al Gameobject que lleva este Script de la MainCamara.
        transform.SetParent(camaraMain.transform);
        //Le aplico una velocidad inicial.
        rig.velocity = new Vector2(-velocidad, 2);

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
            float posicionX = camaraMain.transform.position.x - transform.position.x;
            float posicionY = camaraMain.transform.position.y - transform.position.y;

            if (posicionX > -7)
            {
                //Le aplico una velocidad para que vaya al otro lado.
                rig.velocity = new Vector2(1.5f, rig.velocity.y);
            }
            if (posicionX < -9)
            {
                //Le aplico una velocidad para que vaya al otro lado.
                rig.velocity = new Vector2(-1.5f, rig.velocity.y);
            }

            if (posicionY < -5)
            {
                //Le aplico una velocidad para que vaya hasta abajo.
                rig.velocity = new Vector2(rig.velocity.x, -2);
            }

            if (posicionY > 4)
            {
                //Le aplico una velocidad para que vaya hasta arriba.
                rig.velocity = new Vector2(rig.velocity.x, 2);
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
                    rig.velocity = new Vector2(velocidad, 0);

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
        vidasBoss.text = "6 - " + vidas.ToString();
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
