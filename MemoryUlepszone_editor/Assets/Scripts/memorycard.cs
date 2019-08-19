using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class memorycard : MonoBehaviour {

    [SerializeField] private GameObject cardBack;  // ZMienna zostanie wyświetlona w inspektorze
    [SerializeField] private SceneController controller;

    public void OnMouseDown()
    {
        if(cardBack.activeSelf && controller.canReveal)     //Jeżeli obiekt jest obecnie aktywny i jeżeli bool canReveal zwraca true
        {
            cardBack.SetActive(false);  //ustawienie obiektu jako nieaktywny
            controller.CardRevealed(this);  //Poinformowanie kontrolera o odkryciu TEJ karty
        }
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);
    }
    /*
    [SerializeField] private Sprite image; //pobieranie sprite z inspektora
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = image; // Ustawienie sprite dla komponentu SpriteRenderer
    }
    */

    private int _id;

    public int id
    {
        get { return _id; }
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;

    }

}
