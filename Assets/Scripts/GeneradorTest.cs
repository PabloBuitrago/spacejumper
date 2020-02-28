using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GeneradorTest : MonoBehaviour
{
    GeneradorBoss generadorBoss;

    public GameObject[] obj;

    public GeneradorSecundario generadorSecundario;

    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeHaGanado");
        NotificationCenter.DefaultCenter().AddObserver(this, "ResetearBoss");
        NotificationCenter.DefaultCenter().AddObserver(this, "IncrementarPuntos");
    }

    void PersonajeHaGanado()
    {
        generadorBoss = GetComponent<GeneradorBoss>();
        generadorBoss.vidas = 1;
    }

    public void Resetear()
    {
        SceneManager.LoadScene("TestLevel");
    }

    public void GenerarBoss0()
    {
        //Genero el Gameobject (Boss) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[0], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    public void GenerarBoss1()
    {
        //Genero el Gameobject (Boss) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[1], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    public void GenerarBoss2()
    {
        //Genero el Gameobject (Boss) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[2], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    public void GenerarBoss3()
    {
        //Genero el Gameobject (Boss) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[3], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);

        generadorSecundario.Generar();
    }

    public void GenerarBoss4()
    {
        //Genero el Gameobject (Boss) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[4], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    public void GenerarBoss5()
    {
        //Genero el Gameobject (Boss) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[5], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    public void GenerarBoss6()
    {
        //Genero el Gameobject (Boss) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[6], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    public void GenerarBoss7()
    {
        //Genero el Gameobject (Boss) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[7], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    public void GenerarBoss8()
    {
        //Genero el Gameobject (Boss) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[8], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    public void GenerarBoss9()
    {
        //Genero el Gameobject (Boss) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[9], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }

    public void GenerarBoss10()
    {
        //Genero el Gameobject (Boss) en una posicion aleatoria y centrado en la camara.
        Instantiate(obj[10], new Vector3(transform.position.x + 1, transform.position.y + 1.1f), Quaternion.identity);
    }
}
