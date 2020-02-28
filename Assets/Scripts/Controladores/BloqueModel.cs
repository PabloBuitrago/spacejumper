using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BloqueModel : MonoBehaviour {

    public Text textoPuntos;

    int puntos = -1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish" && puntos < 9)
        {
            puntos++;
            //Se actualiza el marcador de puntos (Este Script es solo para la escena de Ayuda).
            textoPuntos.text = puntos.ToString();
        }

    }
}
