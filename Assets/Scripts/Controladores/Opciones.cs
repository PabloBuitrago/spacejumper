using UnityEngine;
using System.Collections;

public class Opciones : MonoBehaviour {

    public AudioSource audio_boton;

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
