using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joybutton : MonoBehaviour , IPointerUpHandler, IPointerDownHandler
{
    public bool isPressed = false;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per fram
    void Update()
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }
}
