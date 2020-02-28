using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Puntuacion : MonoBehaviour {

    private int puntuacion = 0;
    int aux;
    bool entrar = true;

    public int puntuacionParaBoss = 25;

    public Text marcador;

    public Text puntuacionGameOver;
    public Text bestPuntuacion;
    public Text bonus;

    int porcentaje = 0;

    public GeneradorBoss generadorBoss;

    public int puntuacionMaximaNivel = 170;

    bool sumarPuntuacion = true;

    public int puntosBonus;

    public Nave nave;

    int nivel;

    public GameOver gameOver;

    int enemigos;
    int boss;
    int item;
    int bloques;

    public ControladorPersonaje controladorPersonaje;
    public Disparar disparar;

    public BBDDJuego bbddJuego;

    bool actualiza = false;

    public AudioSource audio_game;
    public AudioSource audio_win;

    AdScript_2 anuncios;

    void Awake()
    {
        anuncios = GetComponent<AdScript_2>();
    }

    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "IncrementarPuntos");
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeHaMuerto");
        NotificationCenter.DefaultCenter().AddObserver(this, "ResetearBoss");

        NotificationCenter.DefaultCenter().AddObserver(this, "Estadisticas");

        nivel = gameOver.nivel;
    }

    void IncrementarPuntos(Notification notificacion)
    {
        //Convierto la variable notificacion a int (entera) para poder trabajar con ella.
        int puntosAIncrementar = (int)notificacion.data;
        //Incremento la puntuacion en la puntuacion total y en una aux que me cuenta los puntos para que vuelva a salir el Boss.
        puntuacion += puntosAIncrementar;
        aux += puntosAIncrementar;

        //Cuando aux es mayor o igual para la puntuacon y entrar sea true.
        if (aux >= puntuacionParaBoss && entrar)
        {
            switch (nivel)
            {
                case 3:
                    if (generadorBoss.vidas == 1)
                    {
                        NotificationCenter.DefaultCenter().PostNotification(this, "GenerarBoss02");
                    }
                    else
                    {
                        NotificationCenter.DefaultCenter().PostNotification(this, "GenerarBoss");
                    }
                    break;
                case 11:
                    int aleatorio = Random.Range(1, 10);
                    if (aleatorio == 1) NotificationCenter.DefaultCenter().PostNotification(this, "GenerarBoss");
                    else
                    {
                        string cero = "";
                        if(aleatorio < 10) cero = "0";
                        NotificationCenter.DefaultCenter().PostNotification(this, "GenerarBoss" + cero + aleatorio);
                    }
                    break;
                default:
                    NotificationCenter.DefaultCenter().PostNotification(this, "GenerarBoss");
                    break;
            }        
    
            //Pongo la variable entrar a true (para que solo entre una vez).
            entrar = false;
        }
        ActualizarMarcador();
    }

    void ActualizarMarcador()
    {
        marcador.text = puntuacion.ToString();
    }

    void PersonajeHaMuerto()
    {
        if (sumarPuntuacion)
        {
            //Calculo el porcentaje dependiendo de los puntos y de la vida del Boss.
            porcentaje = (puntuacion * 100) / puntuacionMaximaNivel;
            //Si tiene 1 vida y la puntuacion pasa la maxima menos un poco, el porcentaje es 99%.
            if ((generadorBoss.vidas == 1 && puntuacion >= puntuacionMaximaNivel) || generadorBoss.vidas == 0)
            {
                porcentaje = 99;
            }
            //Si no le recoge la nave (Aun que haya matado al Boss) su porcentaje sera 99% (Hay que volver a la nave para completar el nivel).
            if (nave.recoger)
            {
                audio_win.Play();

                porcentaje = 100;

                //Como ha ganado doy un bonus de puntos, dependiendo del nivel.
                puntuacion += puntosBonus;
                bonus.text = "BONUS: +" + puntosBonus.ToString();
            }
            else
            {
                audio_game.Play();

                //Estadisticas Muertes;
                EstadoJuego.estadoJuego.muertes++;

                if(EstadoJuego.estadoJuego.muertes % 5 == 0)
                {
                    anuncios.showInterstitialAd();
                }
            }

            if (porcentaje > EstadoJuego.estadoJuego.porcentajeMaximo[nivel])
            {
                //Pongo el porcentaje que ha sacado en la variable de clase EstadoJuego.
                EstadoJuego.estadoJuego.porcentajeMaximo[nivel] = (int)porcentaje;
            }

            //Compruebo el porcentaje que tiene (solucion de fallos de versiones anteriores a la 2.1).
            if (EstadoJuego.estadoJuego.porcentajeMaximo[nivel] > 100)
            {
                EstadoJuego.estadoJuego.porcentajeMaximo[nivel] = 100;
                EstadoJuego.estadoJuego.puntuacionMaxima[nivel] = puntuacionMaximaNivel + puntosBonus;
            }

            if (puntuacion > EstadoJuego.estadoJuego.puntuacionMaxima[nivel])
            {
                //Pongo la puntuacion que ha sacado en la variable de clase EstadoJuego.
                EstadoJuego.estadoJuego.puntuacionMaxima[nivel] = puntuacion;

                actualiza = true;
            }

            //Pongo la puntuacion en el GameOver.
            puntuacionGameOver.text = puntuacion.ToString();
            bestPuntuacion.text = EstadoJuego.estadoJuego.puntuacionMaxima[nivel].ToString();

            //Pongo la variable Reset a 1, para que no salga el Panel Principal del nivel.
            PlayerPrefs.SetInt("Reset", 1);

            //Guardo las estadisticas de la partida.
            EstadoJuego.estadoJuego.saltos += controladorPersonaje.saltos;
            EstadoJuego.estadoJuego.disparos += disparar.disparos;
            EstadoJuego.estadoJuego.enemigos += enemigos;
            EstadoJuego.estadoJuego.boss += boss;
            EstadoJuego.estadoJuego.item += item;
            EstadoJuego.estadoJuego.bloques += bloques;

            //Guardo todas las variables de la clase EstadoJuego.
            EstadoJuego.estadoJuego.Guardar();

            //Si se ha conseguido mas puntuacion que la maxima, la puntacionTotal cambia y se actualiza.
            if (actualiza)
            {
                //Cargo todas las variables.
                EstadoJuego.estadoJuego.Cargar();

                bbddJuego.ActualizarBBDD();
            }

            sumarPuntuacion = false;
        }
    }

    void ResetearBoss()
    {
        //Cambio la variable aux a 0 para que me vuelva a contar para que salga e Boss y entrar a true.
        aux = 0;
        entrar = true;
    }

    void Estadisticas(Notification notificacion)
    {
        //Convierto la variable notificacion a int (entera) para poder trabajar con ella.
        int estadistica = (int) notificacion.data;
        
        switch (estadistica)
        {
            //Enemigos.
            case 0:
                enemigos++;
                break;
            //Boss.
            case 1:
                boss++;
                break;
            //Item.
            case 2:
                item++;
                break;
            //Bloques.
            case 3:
                bloques++;
                break;
        }
    }
}
