using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class ButtonMoveScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler// These are the interfaces the OnPointerUp method requires.
{
    public PlayerController pc;
    public string tipo;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (tipo == "Derecha"){
            pc.OnMoveButton(1);
        }else if (tipo == "Izquierda"){
            pc.OnMoveButton(-1);
        }else if (tipo == "Salto"){
            pc.OnSaltarButton();
        }else{
            pc.OnReiniciar();
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (tipo == "Derecha"){
            pc.OnMoveButton(0);
        }else if (tipo == "Izquierda"){
            pc.OnMoveButton(0);
        }else if (tipo == "Salto"){
            //pc.OnSaltarButton();
        }else{

        }
    }
}