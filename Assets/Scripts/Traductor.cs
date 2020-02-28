using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Traductor : MonoBehaviour {

    public string escena = "Nivel";
    public string escenanivel = "00";

    public Text by;
    public Text play;
    public Text help;
    public Text exit;
    public Text jump;
    public Text shoot;
    public Text space;

    public bool sep1;

    public Text selectlevel;
    public Text puntuaciontotal;
    public Text completado;
    public Text bloqueado;
    public Text nick;
    public Text loading;
    public Text logros;
    public Text stast;
    public Text muertes;
    public Text disparos;
    public Text saltos;
    public Text enemigos;
    public Text jefes;
    public Text bloques;
    public Text diamantes;
    public Text ranking;
    public Text registro;
    public Text intro;
    public Text acept;
    public Text cancel;
    public Text validar;
    public Text opciones;
    public Text opcion1;
    public Text opcion2;
    public Text opcion3;

    public bool sep2;

    public Text nivel;
    public Text vidaboos;
    public Text score;
    public Text limit;
    public Text doble;
    public Text texto1;
    public Text texto2;

    public bool sep3;

    public Text pausa1;
    public Text pausa2;
    public Text pausa3;
    public Text lev;
    public Text plataformas;
    public Text enem;
    public Text boss;
    public Text punt;
    public Text maxpunt;
    public Text reset;
    public Text compl;

    string bytext = "Versión 3.0. de la aplicación.\nPara cualquier consulta,\nmejora o información contactar con: mipanco@gmail.com.";
    string playtext = "Jugar";
    string helptext = "Ayuda";
    string exittext = "Salir";
    string jumptext = "saltar";
    string shoottext = "disparar";
    string spacetext = "espacio";

    string selectleveltext = "Seleciona Nivel";
    string puntuaciontotaltext = "puntuacion\ntotal";
    string completadotext = "completado";
    string bloqueadotext = "bloqueado";
    string nicktext = "nombre:";
    string loadingtext = "cargando...";
    string logrostext = "logros";
    string stasttext = "Estadisticas";
    string muertestext = "muertes:";
    string disparostext = "disparos:";
    string saltostext = "saltos:";
    string enemigostext = "enemigos\nmatados:";
    string jefestext = "jefes\nmatados:";
    string bloquestext = "bloques\n pisados:";
    string diamantestext = "diamantes:";
    string rankingtext = "Click aqui si no te encuentras en el ranking";
    string registrotext = "Bienvenido Astronauta, antes de empezar tendras que elegir un Nombre. \n\nY compite con tu PUNTUACION TOTAL en el Ranking Mundial.";
    string introtext = "Escribe tu nombre";
    string acepttext = "aceptar";
    string canceltext = "cancelar";
    string validartext = "El nombre no puede tener mas de 15 CARACTERES, \nNO puede contener ni espacios ni puntos \ny NO puede estar vacio.";
    string opcionestext = "opciones";
    string opcion1text = "Sonidos y musica";
    string opcion2text = "Quitar musica";
    string opcion3text = "Cambiar nombre";

    string niveltext = "Nivel";
    string vidaboostext = "Vida jefe";
    string scoretext = "Puntos";
    string limittext = "Disparos\nLimitados";
    string dobletext = "Salto\nDoble";
    string texto1text = "Coge diamantes\npara conseguir\npuntos y\nbalas";
    string texto2text = "Tu objetivo es\nmatar al jefe de cada nivel\ny conseguir el maximo de puntos";

    string pausa1text = "PAUSA";
    string pausa2text = "continuar";
    string pausa3text = "menu";
    string levtext = "Nivel ";
    string plataformastext = "Plataformas:";
    string enemtext = "Enemigos:";
    string bosstext = "Jefes:";
    string punttext = "puntos";
    string maxpunttext = "mejor";
    string resettext = "empezar";
    string compltext = "completar";

    void Start () {
        if (Application.systemLanguage.ToString() == "Spanish")
        {
            switch (escena)
            {
                case "Inicio":
                    by.text = bytext;
                    play.text = playtext;
                    help.text = helptext;
                    exit.text = exittext;
                    jump.text = jumptext;
                    shoot.text = shoottext;
                    space.text = spacetext;
                    break;
                case "GameScene":
                    selectlevel.text = selectleveltext;
                    puntuaciontotal.text = puntuaciontotaltext;
                    completado.text = completadotext;
                    bloqueado.text = bloqueadotext;
                    nick.text = nicktext;
                    loading.text = loadingtext;
                    logros.text = logrostext;
                    stast.text = stasttext;
                    muertes.text = muertestext;
                    disparos.text = disparostext;
                    saltos.text = saltostext;
                    enemigos.text = enemigostext;
                    jefes.text = jefestext;
                    bloques.text = bloquestext;
                    diamantes.text = diamantestext;
                    ranking.text = rankingtext;
                    registro.text = registrotext;
                    acept.text = acepttext;
                    cancel.text = canceltext;
                    validar.text = validartext;
                    opciones.text = opcionestext;
                    opcion1.text = opcion1text;
                    opcion2.text = opcion2text;
                    opcion3.text = opcion3text;
                    break;
                case "Help":
                    nivel.text = niveltext;
                    vidaboos.text = vidaboostext;
                    score.text = scoretext;
                    limit.text = limittext;
                    doble.text = dobletext;
                    texto1.text = texto1text;
                    texto2.text = texto2text;
                    break;
                case "Nivel":
                    pausa1.text = pausa1text;
                    pausa2.text = pausa2text;
                    pausa3.text = pausa3text;
                    lev.text = levtext + escenanivel;
                    plataformas.text = plataformastext;
                    enem.text = enemtext;
                    boss.text = bosstext;
                    punt.text = punttext;
                    maxpunt.text = maxpunttext;
                    reset.text = resettext;
                    compl.text = compltext;
                    break;
            }

            
        }
    }
}
