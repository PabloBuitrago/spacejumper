using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Pulsar : MonoBehaviour, IPointerDownHandler{

    public bool pulsado;

    public void OnPointerDown(PointerEventData eventData)
    {
        pulsado = true;
    }
}
