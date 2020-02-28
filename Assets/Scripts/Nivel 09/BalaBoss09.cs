using UnityEngine;
using System.Collections;

public class BalaBoss09 : MonoBehaviour
{

    public float velocidad = 10f;

    public float velocidadVuelta = 30f;

    Rigidbody2D rig;

    float timer;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        //Le aplico una velocidad inicial para que se mueva continuamente.
        rig.velocity = new Vector2(-velocidad, rig.velocity.y);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1.5f)
        {
            //Le aplico una velocidad positiva para que vuelva.
            rig.velocity = new Vector2(velocidadVuelta, rig.velocity.y);

            if(timer > 4f)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeHaMuerto");
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Bala")
        {
            Destroy(other.gameObject);
        }
    }
}

