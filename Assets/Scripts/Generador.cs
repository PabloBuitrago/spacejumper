using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Generador : MonoBehaviour {

    public GameObject[] obj;
    public float tiempoMin = 1.5f;
    public float tiempoMax = 3f;

    public GameObject item;
    public GameObject[] monstruos;

    public bool fin = false;

    float altura;

    bool generar;

    void Start ()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeEmpiezaACorrer");
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeHaMuerto");
    }

    void PersonajeEmpiezaACorrer()
    {
        Generar();
    }

    void PersonajeHaMuerto()
    {
        fin = true;
    }

    public void Generar()
    {
        if (!fin)
        {
            //Genero un aleatorio de 0 a la longitud del array de obj(Array de bloques)
            int aleatorio = Random.Range(0, obj.Length);

            //Instancio un Gameobject del array en un posicion aleatoria (Generada anteriormente).
            Instantiate(obj[aleatorio], transform.position, Quaternion.identity);
            //Invoco la funcion Generar (Para que se forme un bucle) en un parametro de tiempo minimo y maximo.
            Invoke("Generar", Random.Range(tiempoMin, tiempoMax));

            //Si no hay un Gameobject con el tag Boss.
            if (GameObject.FindGameObjectWithTag("Boss") == false)
            {
                //Genero un segundo aleatorio entre 0 y 8.
                int aleatorio2 = Random.Range(0, 8);

                switch (aleatorio2)
                {
                    //Si el aleatorio es 1 o 2 (Un 25%).
                    case 1:
                    case 2:
                        //Instancio el Gameobject item (Diamante) en una posicion 1 mas alta que el bloque.
                        Instantiate(item, new Vector3(transform.position.x, transform.position.y + 1), Quaternion.identity);
                        break;
                    
                    //Si el aleatorio es 3 (Un 12.5%).
                    case 3:
                        generar = true;
                        //Dependiendo del bloque aleatorio generado le indico el tipo de bloque al personaje.                     
                        float suma;
                        switch (obj[aleatorio].name)
                        {
                            case "BloqueMediano":
                            case "BloqueMedianoCae":
                            case "BloqueMedianoDesaparece":
                                suma = Random.Range(0, 1.35f);
                                break;
                            case "BloqueLargo":
                                suma = Random.Range(0, 2.35f);
                                break;
                            case "BloqueCorto":
                            case "BloquePequenoCae":
                                suma = 0;
                                break;
                            case "BloquePequenoMueve":
                                suma = 0;
                                generar = false;
                                break;
                            default:
                                suma = 0;
                                break;
                        }

                        //Instancio del array de monstruos uno aleatorio, en la posicion de la mitad al final del bloque.
                        int aleatorio3 = Random.Range(0, monstruos.Length);
                        //Dependiendo del monstruo sale mas o menos arriba, para que salga alineado con el bloque.
                        switch (monstruos[aleatorio3].name)
                        {
                            case "Alien":
                            case "Astron":
                            case "BichoEscudo":
                                altura = 1.1f;
                                break;
                            case "Alien_Lider":
                            case "BichoInvisible":
                            case "Arma":
                                altura = 1.4f;
                                break;
                            case "Pinchos":
                                altura = 0.8f;
                                break;
                            default:
                                altura = 0;
                                break;
                        }

                        if (generar)
                        {
                            Instantiate(monstruos[aleatorio3], new Vector3(transform.position.x + suma, transform.position.y + altura), Quaternion.identity);
                        }
                        break;
                }
            }
        }      
    }
}
