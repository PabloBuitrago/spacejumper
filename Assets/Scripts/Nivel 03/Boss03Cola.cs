using UnityEngine;
using System.Collections;

public class Boss03Cola : MonoBehaviour {

    public float velocidad = 5f;

    GameObject camaraMain;
    Rigidbody2D rig;

    bool entrar = true;

    void Awake()
    {
        camaraMain = GameObject.FindGameObjectWithTag("MainCamera");
        rig = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //Hago hijo al Gameobject que lleva este Script de la MainCamara.
        transform.SetParent(camaraMain.transform);
        //Le aplico una velocidad inicial.
        rig.velocity = new Vector2(velocidad, rig.velocity.y);
    }

    void Update()
    {
        if (entrar)
        {
            //Calculo la posicion relativa del boss con la camara.
            float posicion = camaraMain.transform.position.x - transform.position.x;

            if (posicion > 11.5f)
            {
                //Le aplico una velocidad para que vaya al otro lado.
                rig.velocity = new Vector2(0.5f, rig.velocity.y);
            }
            
            if (posicion < 11)
            {
                //Le aplico una velocidad para que vaya al otro lado.
                rig.velocity = new Vector2(-0.5f, rig.velocity.y);
            }
        }
    }

    public void Desaparecer()
    {
        //Si tiene mas de una vida no dejo que se mueva.
        entrar = false;
        rig.velocity = new Vector2(-5, 0);

        //Invoco la funcion Morir a los dos segundos (dejandole tiempo para que se vaya).
        Invoke("Morir", 2);
    }

    public void Morir()
    {
        Destroy(gameObject);
    }

}
