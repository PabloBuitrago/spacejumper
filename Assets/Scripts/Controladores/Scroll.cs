using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

	public bool IniciarEnMovimiento = false;
	public float velocidad = 0f;
	private bool enMovimiento = false;
	private float tiempoInicio = 0f;

	void Start () {
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeEmpiezaACorrer");
		NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeHaMuerto");
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeDanado");

        if (IniciarEnMovimiento) {
			PersonajeEmpiezaACorrer();
		}
	}

	void PersonajeHaMuerto()
    {
		enMovimiento = false;
    }

    void PersonajeDanado()
    {
        enMovimiento = false;
    }

    void PersonajeEmpiezaACorrer()
    {
        //Pongo enMovimiento a true (para activar el Scroll).
		enMovimiento = true;
        //Pongo el tiempo de inicio en el actual.
		tiempoInicio = Time.time;
	}
	
	void Update () {
        if (enMovimiento){
            //Activo el Renderer del Scroll acorde con la velocidad del personaje (calculada con el tiempo de inicio y el tiempo actual).
			GetComponent<Renderer>().material.mainTextureOffset = new Vector2(((Time.time - tiempoInicio) * velocidad) % 1, 0);
		}
	}
}
