using UnityEngine;
using System.Collections;

public class Arma : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeDanado");
        }

        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
