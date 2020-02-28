using UnityEngine;
using System.Collections;

public class Disparador01 : MonoBehaviour
{

    public GameObject obj;

    bool entrar = true;

    GameObject generadorPrincipal;
    GeneradorBoss generadorBoss;

    void Awake()
    {
        generadorPrincipal = GameObject.FindGameObjectWithTag("GeneradorPrincipal");
        generadorBoss = generadorPrincipal.GetComponent<GeneradorBoss>();
    }

    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeHaMuerto");

        //Invoco la funcion Generar pasado 3 segundos (para que empiece a disparar cuando se vea por pantalla).
        Invoke("Generar", 3);
    }

    void Generar()
    {
        if (entrar && generadorBoss.vidas == 1)
        {
            //Si hay un 1 de los cuatro numeros aleatorios entra (un 25% de probabilidad).
            if (Random.Range(0, 4) == 1)
            {
                //Instancio el Gameobject en la posicion de este Gameobject.
                Instantiate(obj, transform.position, Quaternion.identity);
            }

            //Vuelvo a invocar la funcion Generar pasado 2 segundos.
            Invoke("Generar", 2);
        }      
    }

    void PersonajeHaMuerto()
    {
        entrar = false;
    }
}
