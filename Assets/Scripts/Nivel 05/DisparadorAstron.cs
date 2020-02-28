using UnityEngine;
using System.Collections;

public class DisparadorAstron : MonoBehaviour
{

    float timer;
    public float tiempoDisparos = 2;

    public GameObject obj;

    void Update()
    {
        timer += Time.deltaTime;

        //Si el timer es mayor o igual al tiempoPulsaciones.
        if (timer >= tiempoDisparos)
        {
            Disparo();

            timer = 0f;
        }
    }

    public void Disparo()
    {
        //Instancio el Gameobject Bala (definido como variable publica), en la posicion de este Gameobject.
        Instantiate(obj, transform.position, Quaternion.identity);
    }
}
