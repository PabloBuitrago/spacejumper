using UnityEngine;
using System.Collections;

public class Bala : MonoBehaviour
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
        rig.velocity = new Vector2(velocidad, rig.velocity.y);

        Destroy(gameObject, 0.8f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Item")
        {
            Destroy(gameObject);
        }
    }
}
