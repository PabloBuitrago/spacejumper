using UnityEngine;
using System.Collections;

public class SeguirPersonaje : MonoBehaviour {

    public Transform personaje;
    public float separacion = 6f;
	
	void Update () {
        //Calculamos la posicion relativa del Player y de la Camara, para moverla con el personaje con una separacion de este al extremos izquierdo de ella.
        transform.position = new Vector3(personaje.transform.position.x + separacion, transform.position.y, transform.position.z);
	}
}
