using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GeneradorBoss : MonoBehaviour {

    public GameObject[] obj;

    public int vidas;
    public Text vidasBoss;

	void Start ()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "GenerarBoss");
        NotificationCenter.DefaultCenter().AddObserver(this, "GenerarBoss02");
        NotificationCenter.DefaultCenter().AddObserver(this, "GenerarBoss03");
        NotificationCenter.DefaultCenter().AddObserver(this, "GenerarBoss04");
        NotificationCenter.DefaultCenter().AddObserver(this, "GenerarBoss05");
        NotificationCenter.DefaultCenter().AddObserver(this, "GenerarBoss06");
        NotificationCenter.DefaultCenter().AddObserver(this, "GenerarBoss07");
        NotificationCenter.DefaultCenter().AddObserver(this, "GenerarBoss08");
        NotificationCenter.DefaultCenter().AddObserver(this, "GenerarBoss09");
        NotificationCenter.DefaultCenter().AddObserver(this, "GenerarBoss10");
    }

    void GenerarBoss()
    {
        //Genero el Gameobject (Boss) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[0], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    void GenerarBoss02()
    {
        //Genero el Gameobject (Boss02) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[1], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    void GenerarBoss03()
    {
        //Genero el Gameobject (Boss02) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[2], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    void GenerarBoss04()
    {
        //Genero el Gameobject (Boss02) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[3], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    void GenerarBoss05()
    {
        //Genero el Gameobject (Boss02) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[4], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    void GenerarBoss06()
    {
        //Genero el Gameobject (Boss02) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[5], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    void GenerarBoss07()
    {
        //Genero el Gameobject (Boss02) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[6], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    void GenerarBoss08()
    {
        //Genero el Gameobject (Boss02) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[7], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    void GenerarBoss09()
    {
        //Genero el Gameobject (Boss02) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[8], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    void GenerarBoss10()
    {
        //Genero el Gameobject (Boss02) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[9], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }
}
