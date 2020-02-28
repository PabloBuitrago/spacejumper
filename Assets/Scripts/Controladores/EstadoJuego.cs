using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

#pragma warning disable 649

public class EstadoJuego : MonoBehaviour {

    public string nombre;
    public string id;

    public string auxnombre;
    public int auxpuntuacionTotal;

    public int puntuacionTotal;

    public int[] puntuacionMaxima = new int[12];
    public int[] porcentajeMaximo = new int[12];

    public int muertes;
    public int disparos;
    public int saltos;
    public int enemigos;
    public int boss;
    public int item;
    public int bloques;

    public static EstadoJuego estadoJuego;
    private String rutaArchivo;

    void Awake()
    {
        //Hago la rula relativa del archivo y pongo el nombre de datos.
        rutaArchivo = Application.persistentDataPath + "/datos.dat";

        //Si existe el estadoJuego lo Destruyo y creo uno nuevo.
        if (estadoJuego == null)
        {
            //El estadoJuego es la que lleva este Script.
            estadoJuego = this;
        }
        else if(estadoJuego != this)
        {
            Destroy(gameObject);
        }

        //Cargo todos los datos del fichero.
        Cargar();
    }

    public void Guardar()
    {
        //Creo o busco un fichero de Bytes.
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(rutaArchivo);

        //Creo un objeto de la clase a guardar.
        DatosAGuardar datos = new DatosAGuardar();

        //Guardo todos los datos de esta clase a la clase a guardar.

        datos.puntuacionTotal = 0;

        for (int i = 0; i <= 11; i++)
        {
            datos.puntuacionMaxima[i] = puntuacionMaxima[i];
            datos.porcentajeMaximo[i] = porcentajeMaximo[i];

            datos.puntuacionTotal += puntuacionMaxima[i];
        }

        datos.muertes = muertes;
        datos.disparos = disparos;
        datos.saltos = saltos;
        datos.enemigos = enemigos;
        datos.boss = boss;
        datos.item = item;
        datos.bloques = bloques;

        datos.nombre = nombre;
        datos.id = id;

        datos.auxnombre = auxnombre;
        datos.auxpuntuacionTotal = auxpuntuacionTotal;

        //Guardo la clase en el fichero.
        bf.Serialize(file, datos);

        //Cierro el fichero
        file.Close();
    }

    public void Cargar()
    {
        //Si existe el archivo.
        if (File.Exists(rutaArchivo))
        {
            //Creo o busco un fichero de Bytes.
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(rutaArchivo, FileMode.Open);

            //Creo un objeto de la clase a guardar a partir de la clase del fichero.
            DatosAGuardar datos = (DatosAGuardar)bf.Deserialize(file);

            puntuacionTotal = datos.puntuacionTotal;

            for (int i = 0; i <= 11; i++)
            {
                puntuacionMaxima[i] = datos.puntuacionMaxima[i];
                porcentajeMaximo[i] = datos.porcentajeMaximo[i];
            }

            muertes = datos.muertes;
            disparos = datos.disparos;
            saltos = datos.saltos;
            enemigos = datos.enemigos;
            boss = datos.boss;
            item = datos.item;
            bloques = datos.bloques;

            nombre = datos.nombre;
            id = datos.id;

            auxnombre = datos.auxnombre;
            auxpuntuacionTotal = datos.auxpuntuacionTotal;

            //Cierro el fichero.
            file.Close();
        }
        else
        {
            //Inicializo todas las variables a 0.

            puntuacionTotal = 0;

            for (int i = 0; i <= 11; i++)
            {
                puntuacionMaxima[i] = 0;
                porcentajeMaximo[i] = 0;
            }

            muertes = 0;
            disparos = 0;
            saltos = 0;
            enemigos = 0;
            boss = 0;
            item = 0;
            bloques = 0;

            nombre = null;
            id = null;

            auxnombre = null; ;
            auxpuntuacionTotal = 0;
        }
    }
}

[Serializable]
class DatosAGuardar
{
    //Variable antiguas (sin importancia).

    public int nivel;
    public bool reset;

    //Variables de la clase que guardo en el fichero.

    public string nombre;
    public string id;

    public string auxnombre;
    public int auxpuntuacionTotal;

    public int puntuacionTotal;

    public int[] puntuacionMaxima = new int[12];
    public int[] porcentajeMaximo = new int[12];

    public int muertes;
    public int disparos;
    public int saltos;
    public int enemigos;
    public int boss;
    public int item;
    public int bloques;
}