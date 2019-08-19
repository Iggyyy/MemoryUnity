using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    [SerializeField] private memorycard orginalCard; //odwołanie do karty znajdującej się na scenie
    [SerializeField] private Sprite[] images; //tablica dla odwołań do zasobów sprite'a
    [SerializeField] private TextMesh scoreLabel;
    [SerializeField] private TextMesh timeLabel;
    [SerializeField] private TextMesh clicksLabel;

    public const int gridRows = 4;              //ilośc kolumn i rzędów
    public const int gridCols = 4;
    public const float offsetX = 2f;            //odległości pomiędzy kartami
    public const float offsetY = 2.1f;

    private memorycard _firstRevealed;
    private memorycard _secondRevealed;

    private int _score = 0;
    private bool IsWon = false;
    private float timer = 0;
    private int clicks = 0;


    void Start () {

        Vector3 startPos = orginalCard.transform.position; //Pobieranie położenia pierwszej karty, reszta będzie definiowana względem niej


        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5,6 ,6, 7, 7 };  //Tablica przechowująca id
        numbers = ShuffleArray(numbers);  //Tasowanie kart za pomocą metody SHuffflearray


        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                memorycard card;    //kontener odwołania do oryginalnej wartości karty lub jej kopii
                if(i==0 && j==0)
                {
                    card = orginalCard;
                }
                else
                {
                    card = Instantiate(orginalCard) as memorycard;
                }


                int index = j * gridCols + i;
                int id = numbers[index];   //pobranie id z potasowanej listy
                
                card.SetCard(id, images[id]);   //wywołanie metody ze skryptu MemoryCard

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);  //przesuniecie karty względem oX i oY
            }
        }

    }
	
    private int[] ShuffleArray(int[] numbers)  //Algorytm tasowania Knutha polegający na losowym zamienianiu indexów tablicy
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;

        }
        return newArray;
    }
	
    public bool canReveal
    {
        get { return _secondRevealed == null; }  //zwraca false jeżeli druga karta została odkryta
    }

    public void CardRevealed(memorycard card)
    {

        if(_firstRevealed == null) //pierwsza odkryta karta
        {
            _firstRevealed = card;
           

        }
        else         //druga odkryta karta, jeżeli pierwsza już została kliknieta
        {
            _secondRevealed = card;
           
            StartCoroutine(CheckMatch()); //wywołanie podprocedury
        }
    }

    private IEnumerator CheckMatch()
    {
        clicks++;
        clicksLabel.text = "Clicks: " + clicks;
        if(_firstRevealed.id == _secondRevealed.id)  //sprawdzanie dopasowania
        {
            _score++;               //inkrementacja wyniku
            scoreLabel.text = "Score: " + _score;  //wyświetlanie aktualnego wyniku
            if(_score == images.Length)
            {
                Debug.Log("s");
                IsWon = true;
               
            }   //sprawdzanie czy gra jest zakonczona
        }
        else
        {
            yield return new WaitForSeconds(.5f);   //zatrzymanie programu
            _firstRevealed.Unreveal();      //ukrycie kart jeżeli nie są matching
            _secondRevealed.Unreveal();

        }
        _firstRevealed = null;    //usunięcie wartości zmiennych
        _secondRevealed = null;
    }

    public void Restart()
    {
        Application.LoadLevel("SampleScene");
        
    }

   
    void Update()
    {
        if (IsWon == false)
        {
            timer += Time.deltaTime;
            timeLabel.text = "Time: " + (int)timer + "s";
        }
    }   
}
