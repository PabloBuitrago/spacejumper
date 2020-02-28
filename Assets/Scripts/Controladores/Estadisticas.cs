using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Estadisticas : MonoBehaviour {

    public Text muertes;
    public Text disparos;
    public Text saltos;
    public Text enemigos;
    public Text boss;
    public Text item;
    public Text bloques;

    int contNiveles;

    public AudioSource audio_boton;

    void Start () {
        muertes.text = EstadoJuego.estadoJuego.muertes.ToString();

        disparos.text = EstadoJuego.estadoJuego.disparos.ToString();

        saltos.text = EstadoJuego.estadoJuego.saltos.ToString();

        enemigos.text = EstadoJuego.estadoJuego.enemigos.ToString();

        boss.text = EstadoJuego.estadoJuego.boss.ToString();

        item.text = EstadoJuego.estadoJuego.item.ToString();

        bloques.text = EstadoJuego.estadoJuego.bloques.ToString();
    }

    public void Monstrar()
    {
        audio_boton.Play();

        gameObject.SetActive(true);
    }

    public void Ocultar()
    {
        audio_boton.Play();

        gameObject.SetActive(false);
    }
}
