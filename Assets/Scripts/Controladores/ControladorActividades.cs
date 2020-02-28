using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class ControladorActividades : MonoBehaviour {

    public GameObject nave;

    public GameObject personaje;

    public AudioSource audio_muerte;

    public AudioMixer mezclador;

    void Start()
    {
        //Pongo el volumen de la musica.
        mezclador.SetFloat("Volumen", PlayerPrefs.GetFloat("Volumen", 0));

        float valor = 0;
        if (PlayerPrefs.GetFloat("Musica", 1) == 0) valor = -80;

        mezclador.SetFloat("Musica", valor);

        //Preparo la publicidad (Intersticial).
        

        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeEmpiezaACorrer");
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeHaMuerto");
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeHaGanado");
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeDanado");
    }

    void PersonajeHaMuerto()
    {
        personaje.SetActive(false);
    }

    void PersonajeDanado()
    {
        audio_muerte.Play();

        //Destruyo los controles del personaje.
        Destroy(personaje.GetComponent<ControladorPersonaje>());
        Destroy(personaje.GetComponentInChildren<Disparar>());

        //Cambio el color.
        personaje.GetComponentInChildren<SpriteRenderer>().color = new Color32(255, 81, 81, 255);

        //Le a�ado un peque�o salto.
        Rigidbody2D rig = personaje.GetComponent<Rigidbody2D>();
        float fuerza = 6f;

        rig.velocity = new Vector2(rig.velocity.x, fuerza);
        rig.AddForce(new Vector2(0, fuerza));

        //Vuelvo Trigger el Collider del Cuerpo y destruyo el del Pie (el que cuenta los puntos).
        personaje.GetComponentInChildren<BoxCollider2D>().isTrigger = true;
        Destroy(personaje.GetComponentInChildren<CircleCollider2D>());
    }

    void PersonajeHaGanado()
    {
        //Busca el Gameobject con el tag Player.
        if(GameObject.FindGameObjectWithTag("Player"))
        {
            //Inicio la animacion del Gameobject nave con el Trigger recojer.
            nave.GetComponent<Animator>().SetTrigger("recojer");
        }      
    }
}
