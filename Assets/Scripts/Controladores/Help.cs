using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Help : MonoBehaviour {

    public AudioMixer mezclador;

    AudioSource audio_boton;

    void Start()
    {
        audio_boton = GetComponent<AudioSource>();

        //Pongo el volumen de la musica.
        mezclador.SetFloat("Volumen", PlayerPrefs.GetFloat("Volumen", 0));

        float valor = 0;
        if (PlayerPrefs.GetFloat("Musica", 1) == 0) valor = -80;

        mezclador.SetFloat("Musica", valor);
    }

    void Update()
    {
        //Si presionan el boton ESC.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            audio_boton.Play();
            SceneManager.LoadScene("Inicio");
        }
    }

    public void Test()
    {
        SceneManager.LoadScene("TestLevel");
    }
}
