using UnityEngine;
using System.Collections;

public class GeneradorSecundario : MonoBehaviour {

    public GameObject obj;
    public GameObject obj2;

    public void Generar()
    {
        //Genero el Gameobject (Boss) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj2, new Vector3(transform.position.x -2, transform.position.y + 3), Quaternion.identity);
    }

    public void Generar02()
    {
        //Genero el Gameobject (Boss) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj, new Vector3(transform.position.x - 2, transform.position.y - 2), Quaternion.identity);
    }
}
