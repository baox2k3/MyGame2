using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Reloadbutton : MonoBehaviour , IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        UI.ínstance.AttempToReload();
        
        gameObject.SetActive(false);
    }
}
