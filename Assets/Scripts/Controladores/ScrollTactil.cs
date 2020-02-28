using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class ScrollTactil : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject mainCamara;

    GameScene gameScene;

    float puntoInicialX;
    float puntoFinalX;

    public AudioSource audio_boton;

    bool mover = false;

    public int velocidad;

    float posicionCamaraX;
    float translacion;

    void Awake()
    {
        gameScene = mainCamara.GetComponent<GameScene>();
    }

    void Update()
    {
        if (mover)
        {
            translacion = (Input.mousePosition.x - puntoInicialX) / -velocidad;

            if((translacion > 0 && gameScene.nivel_sel < gameScene.niveles - 1) ||
                (translacion < 0 && gameScene.nivel_sel > 0))
            {
                gameScene.Ocultar();

                mainCamara.transform.position = new Vector3(posicionCamaraX + translacion, 0, -10);
            }
        }

        if (Input.GetButtonDown("Fire1"))
            Pulsar(); 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Guardo la posicion cuando pulsa la pantalla.
        puntoInicialX = Input.mousePosition.x;

        posicionCamaraX = mainCamara.transform.position.x;

        mover = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mover = false;

        //Guardo la posicion cuando deja de pulsar la pantalla.
        puntoFinalX = Input.mousePosition.x;

        //Compruebo cuanto se ha movido desde que pulso hasta que solto.
        float comprobacion = puntoFinalX - puntoInicialX;
        //Si se ha movido mas de 50 pixel activo la comprobacion.
        if (Math.Abs(comprobacion) > 50)
        {
            if (comprobacion > 0)
            {
                gameScene.PulsarIzquierda();
            }
            else
            {
                gameScene.PulsarDerecha();
            }
        }
        else
        {
            Pulsar();
        }
        
    }

    void Pulsar()
    {
        audio_boton.Play();

        int nivel = gameScene.nivel_sel;
        gameScene.SelecionNivel(nivel);

        bool entrar = true;

        if (nivel >= 1 && nivel <= 5 && !gameScene.entrar1_5) entrar = false;
        if (nivel >= 6 && nivel <= 10 && !gameScene.entrar6_10) entrar = false;
        if (nivel == 11 && !gameScene.entrar11) entrar = false;       

        if (entrar)
        {
            //Pongo cero al nivel de un digito
            string cero = "";
            if (nivel < 10) cero = "0";

            SceneManager.LoadScene("Level" + cero + nivel);

            //Pongo el ultimo nivel que ha jugado.
            PlayerPrefs.SetInt("Niveles", nivel);

            //Guardo la clase EstadoJuego (con el nivel).
            EstadoJuego.estadoJuego.Guardar();
        }        
    }
}
