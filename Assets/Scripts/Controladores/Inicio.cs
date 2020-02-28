using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Inicio : MonoBehaviour {

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

    public void Jugar()
    {
        audio_boton.Play();

        SceneManager.LoadScene("GameScene");
    }

    public void Help()
    {
        audio_boton.Play();

        SceneManager.LoadScene("Help");
    }

    public void Salir()
    {
        audio_boton.Play();

        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void Update()
    {
        //Si presionan el boton ESC.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Salgo de la aplicacion (en el Debuger de Unity no funciona).
            Application.Quit();
        }
    }
}
