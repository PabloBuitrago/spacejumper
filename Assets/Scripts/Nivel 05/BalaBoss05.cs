using UnityEngine;
using System.Collections;

public class BalaBoss05 : MonoBehaviour {

    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.5f)
        {
            gameObject.SetActive(false);
            timer = 0;
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
