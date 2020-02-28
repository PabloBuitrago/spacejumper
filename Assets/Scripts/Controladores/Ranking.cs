using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ranking : MonoBehaviour {

    public Text puntuacionTotal;

    public GameObject registrar;
    public GameObject opciones;
    public Text textoRegistrar;

    public InputField cajaTexto;

    public Text jugador;

    public GameObject validacion;
    public GameObject botonatras;

    public BBDDJuego bbddJuego;

    public AudioSource audio_boton;

    public GameObject conectando;

    public GameObject panelRanking;

    string texto1 = "Welcome back Astronaut, here you will be able to change your Nickname. \n\nYour Nickname is already chosen by another player. Please choose another one.";
    string texto2 = "Welcome back Astronaut, here you will be able to change your Nickname.";

    void Start()
    {
        if (Application.systemLanguage.ToString() == "Spanish")
        {
            texto1 = "Bienvenido Astronauta, aqui podras cambiar tu Nombre. \n\nTu Nombre ya esta usado por otro jugador. Por favor escoge uno diferente.";
            texto2 = "Bienvenido Astronauta, aqui podras cambiar tu Nombre.";
        }

        //Pongo el nombre del jugador;
        jugador.text = EstadoJuego.estadoJuego.nombre;

        //Pongo la puntuacion total del jugador.
        PuntuacionTotal();

        //Muestro la posicion del jugador.
        bbddJuego.MonstrarPosicion();

        //Compruebo si tengo que actualizar.
        Comprobar();
    }

    public void Monstrar()
    {
        audio_boton.Play();

        bbddJuego.pulsado = true;

        //Compruebo si tengo que actualizar.
        Comprobar();
    }

    public void Ocultar()
    {
        audio_boton.Play();

        panelRanking.SetActive(false);
    }

    void Comprobar()
    {
        //Compruebo si ha cambiado algun campo de la BBDD
        //o si acaba de empezar y no tiene nombre o ha pulsado la pantalla, si es asi Actualizo la BBDD.
        if (EstadoJuego.estadoJuego.puntuacionTotal != EstadoJuego.estadoJuego.auxpuntuacionTotal
            || EstadoJuego.estadoJuego.nombre != EstadoJuego.estadoJuego.auxnombre
            || EstadoJuego.estadoJuego.nombre == "" || EstadoJuego.estadoJuego.nombre == null
            || bbddJuego.pulsado)
        {
            string nombre = EstadoJuego.estadoJuego.nombre;

            if (nombre == "" || nombre == null)
            {
                Debug.Log("Registrate");

                //Pantalla de introduccion de nombre.
                registrar.SetActive(true);
            }
            else
            {
                //Actualizamos la BBDD;
                bbddJuego.ActualizarBBDD();
            }
        }
    }

    public void Registro()
    {
        audio_boton.Play();

        string nombre = cajaTexto.text;

        if (nombre == "" || nombre == null || nombre.Length > 15 || nombre.Length < 3 || nombre.Contains(" ") || nombre.Contains("."))
        {
            //Texto de Validacion.
            validacion.SetActive(true);

            Invoke("Desaparecer", 8);
        }
        else
        {
            //Compruebo si tiene un id.
            string id = EstadoJuego.estadoJuego.id;
            if (id == null || id == "") EstadoJuego.estadoJuego.id = SystemInfo.deviceUniqueIdentifier;
            //Le asigno el nombre.
            EstadoJuego.estadoJuego.nombre = nombre;

            //Guardo la variable de la clase EstadoJuego.
            EstadoJuego.estadoJuego.Guardar();

            registrar.SetActive(false);
            opciones.SetActive(false);

            //Pongo el nombre del jugador;
            jugador.text = EstadoJuego.estadoJuego.nombre;

            Comprobar();
        }
    }

    void Desaparecer()
    {
        validacion.SetActive(false);
    }

    public void NombreExiste()
    {
        textoRegistrar.text = texto1;
        cajaTexto.text = EstadoJuego.estadoJuego.nombre;

        botonatras.SetActive(false);

        registrar.SetActive(true);
    }

    public void CambiarNombre()
    {
        audio_boton.Play();

        textoRegistrar.text = texto2;
        cajaTexto.text = EstadoJuego.estadoJuego.nombre;

        botonatras.SetActive(true);
        
        registrar.SetActive(true);
    }

    public void Atras()
    {
        audio_boton.Play();

        registrar.SetActive(false);
    }

    void PuntuacionTotal()
    {
        //Pongos ceros en el Score Total.
        string puntuacion = EstadoJuego.estadoJuego.puntuacionTotal.ToString();
        int digitos;
        string ceros = "";

        digitos = puntuacion.Length;

        for (int i = 1; i <= 6 - digitos; i++)
        {
            ceros += "0";
        }

        puntuacionTotal.text = (ceros + puntuacion).ToString();
    }
}
