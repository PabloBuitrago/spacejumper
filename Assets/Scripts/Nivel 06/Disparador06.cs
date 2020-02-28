using UnityEngine;
using System.Collections;

public class Disparador06 : MonoBehaviour {

    public GameObject obj;

    bool entrar = true;

    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeHaMuerto");

        //Invoco la funcion Generar pasado 3 segundos (para que empiece a disparar cuando se vea por pantalla).
        Invoke("Generar", 3);
    }

    void Generar()
    {
        if (entrar)
        {
            //Si hay un 1 de los cuatro numeros aleatorios entra (un 25% de probabilidad).
            if (Random.Range(0, 5) == 1)
            {

                for (int i = 0; i <= 5; i++)
                {
                    Invoke("DisparosSeguidos", i * 0.1f);
                }
            }

            //Vuelvo a invocar la funcion Generar pasado 2 segundos.
            Invoke("Generar", 2);
        }
    }

    void DisparosSeguidos()
    {
        //Instancio el Gameobject en la posicion de este Gameobject.
        Instantiate(obj, transform.position, Quaternion.identity);
    }

    void PersonajeHaMuerto()
    {
        entrar = false;
    }
}
