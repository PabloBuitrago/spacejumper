using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public int nivel;

    public Text texto;
    public GameObject gameOver;
    public Animator anim;
    public Text vidaboss;
    public GameObject pausa;
    public GameObject balas;
    public GameObject slider;

    Pausa pausaScript;

    public AudioSource audio_boton;

    void Awake()
    {
        //Busco el componente Pausa en el hijo del Gameobject que lleva este Script.
        pausaScript = gameOver.GetComponent<Pausa>();
    }

    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeHaMuerto");
    }

    void Update()
    {
        //Si presiona el boton ESC.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Si no esta en Pausa.
            if (texto.enabled == true)
            {
                //Pausamos el juego.
                pausaScript.Pausar();
            }
            else
            {
                SceneManager.LoadScene("GameScene");
            } 
        }

        if (Input.GetButtonDown("Fire1") && texto.enabled == false)
        {
            Reset();
        }
    }

    void PersonajeHaMuerto()
    {
        texto.enabled = false;
        vidaboss.enabled = false;
        pausa.SetActive(false);
        balas.SetActive(false);
        slider.SetActive(false);

        //Iniciamos el trigger de gameOver del Animator.
        anim.SetTrigger("gameOver");
    }

    public void Reset()
    {
        //Activamos la escena de nivel en el que nos encontramos (esta funcion se activa por medio de boton).
        string cero = "";
        if (nivel < 10) cero = "0";
        SceneManager.LoadScene("Level" + cero + nivel);
    }

    //(esta funcion se activa por medio de boton).
    public void Atras()
    {
        //Reanudo el tiempo del juego.
        Time.timeScale = 1;

        SceneManager.LoadScene("GameScene");
    }
}
