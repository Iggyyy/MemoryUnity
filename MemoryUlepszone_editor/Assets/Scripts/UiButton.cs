using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButton : MonoBehaviour {

    [SerializeField] private GameObject targetObject;
    [SerializeField] private string targetMessage;
    public Color highlightColor = Color.cyan;

    public void OnMouseEnter()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null) 
        {
            sprite.color = highlightColor;      //podświetlenie przycisku po najechaniu kursorem
        }
    }
    public void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.white;         //przywrócenie domyslnego koloru
        }
    }

    public void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);  //przycisk po kliknięciu się zwiększy
    }

    public void OnMouseUp()
    {
        transform.localScale = Vector3.one; //przywrócenie wartości 1 dla wszystkich współrzędnych
        if(targetObject != null)
        {
            targetObject.SendMessage(targetMessage);
        }
    }
    

}
