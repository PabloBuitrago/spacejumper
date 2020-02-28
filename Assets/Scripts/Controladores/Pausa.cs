using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Pausa : MonoBehaviour {

    public GameObject panelPulsar;
    public GameObject panelNivel;

    public InicoLevel inicioLevel;

    public bool paused;

    bool entrar = true;

    public AudioMixer mezclador;

    float volumen;

    AudioSource audio_boton;

    void Start()
    {
        audio_boton = GetComponent<AudioSource>();

        volumen = PlayerPrefs.GetFloat("Volumen");

        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeHaMuerto");
    }

    void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus && entrar)
            Pausar();
    }

    public void Pausar()
    {
        audio_boton.Play();

        if (panelPulsar.activeSelf == false)
        {
            mezclador.SetFloat("Volumen", volumen - 10);

            //Paro el tiempo del juego.
            Time.timeScale = 0;

            panelPulsar.SetActive(true);
            panelNivel.SetActive(false);
        }  
    }

    public void Reanudar()
    {
        audio_boton.Play();

        mezclador.SetFloat("Volumen", volumen);

        //Reanudo el tiempo del juego.
        Time.timeScale = 1;
        //Desactivo el panel de Pausa y activo el Panel del Nivel (en caso de que estuviera activo).
        panelPulsar.SetActive(false);
        if(inicioLevel.entrar && PlayerPrefs.GetInt("Reset") == 0) panelNivel.SetActive(true);
    }

    void PersonajeHaMuerto()
    {
        entrar = false;
    }
}
