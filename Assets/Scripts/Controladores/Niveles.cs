using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Niveles : MonoBehaviour {

    GameObject camaraMain;

    int nivel;

    Text texto;
    Image image;

    void Awake()
    {
        camaraMain = GameObject.FindGameObjectWithTag("MainCamera");

        texto = GetComponentInChildren<Text>();
        image = GetComponent<Image>();
    }

    void Start()
    {
        nivel = Convert.ToInt32(gameObject.name);

        string cero = "";
        if (nivel < 10) cero = "0";

        texto.text = cero + nivel.ToString();
    }

    void Update()
    {
        //Compruebo en que nivel esta.
        if((camaraMain.transform.position.x / 15) == nivel)
        {
            image.enabled = false;
        }
        else
        {
            image.enabled = true;
        }
    }
}
