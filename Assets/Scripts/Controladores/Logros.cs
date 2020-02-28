using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Logros : MonoBehaviour {

    public Sprite si;
    public Sprite no;

    public GameObject[] niveles;

    public AudioSource audio_boton;

    void Start()
    {
        for (int i = 0; i <= 10; i++)
        {
            int porcentaje = EstadoJuego.estadoJuego.porcentajeMaximo[i];

            niveles[i].GetComponentInChildren<Text>().text = porcentaje.ToString() + " / 100%";

            if (porcentaje == 100)
            {
                niveles[i].GetComponentsInChildren<Image>()[2].sprite = si;
            }
            else
            {
                niveles[i].GetComponentsInChildren<Image>()[2].sprite = no;
            }
        }

        if(EstadoJuego.estadoJuego.puntuacionMaxima[11] > 5000)
        {
            niveles[11].GetComponentsInChildren<Image>()[2].sprite = si;
        }
        else
        {
            niveles[11].GetComponentsInChildren<Image>()[2].sprite = no;
        }
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
