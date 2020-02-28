using UnityEngine;
using System.Collections;

public class BalaAstron : MonoBehaviour
{

    public float velocidad = 10f;

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

        Destroy(gameObject, 1.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeDanado");
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Bala")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
