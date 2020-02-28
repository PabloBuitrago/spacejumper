using UnityEngine;
using System.Collections;

public class Disparador02 : MonoBehaviour {

    public GameObject obj;

    bool entrar = true;

    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeHaMuerto");

        //Invoco la funcion Generar pasado 3 segundos (para que empiece a disparar cuando se vea por pantalla).
        Invoke("Generar", 4);
    }

    void Generar()
    {
        if (entrar)
        {
            //Si hay un 1 de los dos numeros aleatorios entra (un 50% de probabilidad).
            if (Random.Range(0, 2) == 1)
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
