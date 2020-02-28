using UnityEngine;
using System.Collections;

public class InicoLevel : MonoBehaviour {

    public GameObject panel;

    public GameObject nave;
    public GameObject player;

    public bool entrar = true;

    public AudioSource audio_salto;

    public AudioSource audio_boton;

    void Start()
    {
        //Si reset es true (es la primera partida).
        if (PlayerPrefs.GetInt("Reset") == 0)
        {
            panel.SetActive(true);
        }
        else
        {
            player.SetActive(true);
            //Coloco al Player encima del primer bloque.
            player.transform.position = new Vector3(player.transform.position.x, -3, player.transform.position.z);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && panel.activeSelf)
        {
            PulsadoNivel();
        }
    }

    public void PulsadoNivel()
    {
        audio_boton.Play();

        panel.SetActive(false);
        entrar = false;

        //Activo el trigger abajo del Animator.
        nave.GetComponent<Animator>().SetTrigger("abajo");

        //Invoco la funcion SoltarPlayer al medio segundo (justo cuando la nave se para).
        Invoke("SoltarPlayer", 0.5f);
    }

    void SoltarPlayer()
    {
        player.SetActive(true);

        audio_salto.Play();
    }
}
