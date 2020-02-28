using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BBDDJuego : MonoBehaviour {

    string URLactualizar = "http://mipandco.esy.es/spacejumper/actualizar.php";
    string URLmonstrar = "http://mipandco.esy.es/spacejumper/ranking.php";
    string URLranking = "http://mipandco.esy.es/spacejumper/puntuaciones.php";
    string URLposicion = "http://mipandco.esy.es/spacejumper/posiciones.php";
    string clave = "uOcusP06rdJbMBXLxoWs";

    public bool nivel = true;

    public GameObject conectando;

    public Animator anim;

    public bool pulsado = false;

    public bool sinConexion = false;
 
    public GameObject panelRanking;

    public GameObject scroll;
    Image[] contornos;
    Text[] contornosTexto;

    public Ranking ranking;

    public Text posicion;

    int marcado = 0;

    public void ActualizarBBDD()
    {
        StartCoroutine("Actualizar");
    }

    public void MonstrarPosicion()
    {
        StartCoroutine("Posiciones");
    }

    private IEnumerator Actualizar()
    {
        conectando.SetActive(true);

        //Reseteamos la conexion
        sinConexion = false;

        //Le damos un tiempo para que conecte.
        int tiempo = 4;
        if (pulsado) tiempo = 8;
        Invoke("NoHayConexion", tiempo);

        string act = URLactualizar + "?" + "clave=" + WWW.EscapeURL(clave) + "&" + "id=" + WWW.EscapeURL(EstadoJuego.estadoJuego.id) + "&" + "Nombre=" + WWW.EscapeURL(EstadoJuego.estadoJuego.nombre) + "&" + "Puntuacion=" + WWW.EscapeURL(EstadoJuego.estadoJuego.puntuacionTotal.ToString());

        WWW urlData = new WWW(act);

        yield return urlData;

        Debug.Log(urlData.text);

        if (!sinConexion)
        {
            switch (urlData.text)
            {
                case "Update Correcto":
                case "Alta Correcta":
                    //Guardo las estadisticas (para que solo entre una vez);
                    EstadoJuego.estadoJuego.auxnombre = EstadoJuego.estadoJuego.nombre;
                    EstadoJuego.estadoJuego.auxpuntuacionTotal = EstadoJuego.estadoJuego.puntuacionTotal;

                    //Guardo la variable de la clase EstadoJuego.
                    EstadoJuego.estadoJuego.Guardar();

                    //Compruebo si esta en el GameScene o si esta en un Nivel.
                    if (!nivel)
                    {
                        anim.SetBool("Conexion", true);
                    }


                    if (pulsado)
                    {
                        StartCoroutine("Posiciones");

                        //Para monstrarlo dentro del juego.
                        string rank = URLranking + "?" + "clave=" + WWW.EscapeURL(clave);

                        WWW urlRank = new WWW(rank);

                        yield return urlRank;

                        string cadenaWeb = urlRank.text;

                        string[] players = cadenaWeb.Split(' ');

                        int pos = 0;

                        contornos = scroll.GetComponentsInChildren<Image>();

                        //Cambio de color el contorno anterior.
                        contornos[marcado].color = new Color32(37, 88, 98, 255);

                        for (int i = 0; i < players.Length - 1; i += 3)
                        {
                            pos++;

                            Image aux = contornos[pos - 1];

                            aux.enabled = true;

                            if (EstadoJuego.estadoJuego.id == players[i])
                            {
                                //Cambio el controno del jugador.
                                aux.color = new Color32(116, 119, 125, 255);
                                //Guardo la posicion del contorno marcado.
                                marcado = pos - 1;
                            }

                            contornosTexto = aux.GetComponentsInChildren<Text>();


                            contornosTexto[1].text = pos + ".";
                            contornosTexto[2].text = players[i + 1];
                            contornosTexto[0].text = players[i + 2];
                        }

                        panelRanking.SetActive(true);
                    }
                    break;
                case "Nombre Existe":
                    if (!nivel) ranking.NombreExiste();
                    break;
                default:
                    NoHayConexion();
                    break;
            }
        }

        pulsado = false;

        conectando.SetActive(false);
    }

    private IEnumerator Posiciones()
    {
        string act = URLposicion + "?" + "clave=" + WWW.EscapeURL(clave) + "&" + "Puntuacion=" + WWW.EscapeURL(EstadoJuego.estadoJuego.puntuacionTotal.ToString());

        WWW urlPos = new WWW(act);

        yield return urlPos;

        if (urlPos.text != "")
        {
            if (urlPos.text.Length > 3)
                posicion.text = "+999";
            else
                posicion.text = urlPos.text + ".";
        }
        else
        {
            posicion.text = "??.";
        }     

    }

    public void NoHayConexion()
    {
        sinConexion = true;

        //Compruebo si esta la pantalla de carga.
        if (conectando.activeSelf)
        {
            //Compruebo si esta en el GameScene o si esta en un Nivel.
            if (!nivel)
            {
                anim.SetBool("Conexion", false);
            }

            Debug.Log("Sin conexion");
        }

        conectando.SetActive(false);
    }

    public void Internet()
    {
        //Me muestra la pagina web del Ranking.
        Application.OpenURL(URLmonstrar + "?" + "id=" + EstadoJuego.estadoJuego.id);
    }
}
