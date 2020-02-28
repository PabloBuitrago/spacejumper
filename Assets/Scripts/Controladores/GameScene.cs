using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameScene : MonoBehaviour {

    public float velocidad = 4;

    public GameObject flechaD;
    public GameObject flechaI;

    public Text textoNivel;
    public int niveles;

    public int[] arrayNiveles;

    public Text bestPuntuacion;
    public Text bestPorcentaje;

    string score = "SCORE ";
    string porcentaje = " %";
    string traducion = "LEVEL ";

    public GameObject completado;
    public GameObject bloqueado;

    public GameObject holograma;

    public GameObject[] panelNivel1_5;
    public GameObject[] panelNivel6_10;
    public GameObject panelNivel11;

    public bool entrar1_5 = true;
    public bool entrar6_10 = true;
    public bool entrar11 = true;

    bool activar;

    public Slider scrollNiveles;

    public int nivel_sel;

    public GameObject estadisticas;
    public GameObject logros;
    public GameObject opciones;
    public GameObject cambio;
    public GameObject ranking;

    public Slider slider_volumen;
    public AudioMixer mezclador;

    public Toggle checkbox;

    public AudioSource audio_boton;

    public float flecha;
    bool activado = false;

    void Start()
    {
        if (Application.systemLanguage.ToString() == "Spanish")
        {
            score = "PUNTOS ";
            traducion = "NIVEL ";
        }

        arrayNiveles = new int[niveles];

        int acum = 0;

        for (int i = 0; i < niveles; i++)
        {
            arrayNiveles[i] = acum;
            acum += 15;
        }

        //Pongo el volumen de la musica.
        float valor = PlayerPrefs.GetFloat("Musica", 1);

        if (valor == 0)
        {
            checkbox.isOn = true;
            mezclador.SetFloat("Musica", -80);
        }
        else
        {
            checkbox.isOn = false;
            mezclador.SetFloat("Musica", 0);
        }

        //Pongo el volumen del juego general.
        valor = PlayerPrefs.GetFloat("Volumen", 0);
        mezclador.SetFloat("Volumen", valor);

        slider_volumen.value = valor;       

        //Pongo la variable Reset a 0 (para saber que he salido del nivel).
        PlayerPrefs.SetInt("Reset", 0);

        //Compruebo si ha completado el Nivel 0.
        if (EstadoJuego.estadoJuego.porcentajeMaximo[0] != 100)
        {
            entrar1_5 = false;

            for(int i = 0; i < 5; i++)
            {
                panelNivel1_5[i].SetActive(true);
            }
        }

        //Compruebo si ha completado los Niveles 1 al 5.
        int k = 0;
        do
        {
            k++;

            if (EstadoJuego.estadoJuego.porcentajeMaximo[k] != 100) entrar6_10 = false;

        } while (k < 5 && entrar6_10) ;

        if (!entrar6_10)
        {
            for (int i = 0; i < 5; i++)
            {
                panelNivel6_10[i].SetActive(true);
            }
        }

        //Compruebo si ha completado los Niveles 6 al 10.
        int j = 5;
        do
        {
            j++;

            if (EstadoJuego.estadoJuego.porcentajeMaximo[j] != 100) entrar11 = false;

        } while (j < 10 && entrar11);

        if (!entrar11)
        {
            panelNivel11.SetActive(true);
        }       

        //Localizo en ultimo nivel que ha jugado.
        nivel_sel = PlayerPrefs.GetInt("Niveles");
        //Guardo la psocion del Scroll y la guardo en una variable.
        scrollNiveles.value = nivel_sel;

        //Posiciono la camra dependiendo del ultimo nivel que ha jugado.
        transform.position = new Vector3(arrayNiveles[nivel_sel], transform.position.y, transform.position.z);

        SelecionNivel(nivel_sel);
    }
	
	void Update () {
        flecha = Input.GetAxisRaw("Horizontal");

        //Si presiona el boton ESC.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estadisticas.activeSelf || logros.activeSelf || opciones.activeSelf || ranking.activeSelf)
            {
                estadisticas.SetActive(false);
                logros.SetActive(false);
                ranking.SetActive(false);
                if(!cambio.activeSelf) opciones.SetActive(false);
            }
            else
            {
                SceneManager.LoadScene("Inicio");
            }           
        }

        if (flecha > 0 && activado == true)
        {
            activado = false;
            PulsarDerecha();
        }            

        if (flecha < 0 && activado == true)
        {
            activado = false;
            PulsarIzquierda();
        }
            
    }

    public void PulsarDerecha()
    {
        //Si es el ultimo nivel.
        if (nivel_sel == niveles - 1)
        {
            transform.position = new Vector3(arrayNiveles[niveles - 1], 0, -10);

            SelecionNivel(nivel_sel);
        }
        else
        {
            nivel_sel++;

            StartCoroutine(MoverCamara(true));
        }     
    }

    public void PulsarIzquierda()
    {
        //Si es el primer nivel.
        if (nivel_sel == 0)
        {
            transform.position = new Vector3(0, 0, -10);

            SelecionNivel(nivel_sel);
        }
        else
        {
            nivel_sel--;            

            StartCoroutine(MoverCamara(false));
        }
    }

    IEnumerator MoverCamara(bool derecha)
    {
        Ocultar();

        if (derecha)
        {
            while (transform.position.x <= (arrayNiveles[nivel_sel] - 2))
            {
                transform.Translate(velocidad * Time.deltaTime, 0, 0);
                yield return null;
            }  
        }
        else
        {
            while (transform.position.x >= (arrayNiveles[nivel_sel] + 2))
            {
                transform.Translate(-velocidad * Time.deltaTime, 0, 0);
                yield return null;
            }
        }

        SelecionNivel(nivel_sel);
    }

    public void SelecionNivel(int nivel)
    {
        audio_boton.Play();

        scrollNiveles.value = nivel;

        if (nivel_sel >= 1 && nivel_sel <= 5 && !entrar1_5) bloqueado.SetActive(true);
        if (nivel_sel >= 6 && nivel_sel <= 10 && !entrar6_10) bloqueado.SetActive(true);
        if (nivel_sel == 11 && !entrar11) bloqueado.SetActive(true);

        //Pongo cero al nivel de un digito
        string cero = "";
        if (nivel_sel < 10) cero = "0";

        textoNivel.text = traducion + cero + nivel_sel;          

        //Compruebo si el nivel esta bloqueado.
        activar = true;
        if (nivel_sel == 0) activar = false;
        flechaI.SetActive(activar);

        activar = true;
        if (nivel_sel == niveles - 1) activar = false;
        flechaD.SetActive(activar);

        bestPuntuacion.text = score + EstadoJuego.estadoJuego.puntuacionMaxima[nivel_sel].ToString();
        if (nivel_sel != 11) bestPorcentaje.text = EstadoJuego.estadoJuego.porcentajeMaximo[nivel_sel].ToString() + porcentaje;
        else bestPorcentaje.text = "Inf %";

        transform.position = new Vector3(arrayNiveles[nivel], 0, -10);

        Monstrar();
    }

    public void Ocultar()
    {
        holograma.SetActive(false);

        completado.SetActive(false);

        bloqueado.SetActive(false);

        textoNivel.enabled = false;

        //Dejo solo el texto de SCORE y el texto %.
        bestPorcentaje.text = porcentaje;
        bestPuntuacion.text = score;       
    }

    void Monstrar()
    {
        //Activo todo el interface de Texto.
        holograma.SetActive(true);

        //Compruebo si tiene el 100% del nivel.
        if (bestPorcentaje.text == "100 %")
        {
            completado.SetActive(true);
        }

        textoNivel.enabled = true;

        activado = true;
    }

    public void CambiarNivel()
    {
        //Oculto todo.
        Ocultar();

        nivel_sel = (int)scrollNiveles.value;

        //Posiciono la camara dependiendo del nivel.
        transform.position = new Vector3(arrayNiveles[nivel_sel], transform.position.y, transform.position.z);

        //Muestro informacion con el nivel selecionado.
        SelecionNivel(nivel_sel);
    }

    public void CambiarVolumen()
    {
        float valor = slider_volumen.value;

        if (valor <= -20) valor = -80;

        PlayerPrefs.SetFloat("Volumen", valor);
        
        mezclador.SetFloat("Volumen", valor);
    }

    public void CambiarMusica()
    {
        audio_boton.Play();

        if (checkbox.isOn)
        {
            mezclador.SetFloat("Musica", -80);
            PlayerPrefs.SetFloat("Musica", 0);
        }
        else
        {
            mezclador.SetFloat("Musica", 0);
            PlayerPrefs.SetFloat("Musica", 1);
        }        
    }
}
