using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardController : MonoBehaviour
{
    [SerializeField] Sprite btnBgImage;
    [SerializeField] List<Button> btns = new List<Button>();

    public Sprite[] cards;
    public List<Sprite> gamePuzzlesP1 = new List<Sprite>(); 

    public List<Sprite> gamePuzzlesP2;

    public bool isPlayer1Active = false;
    public bool isPlayer2Active = false;

    bool firstGuessP1, secondGuessP1;
    bool firstGuessP2, secondGuessP2;

    int countGuesses;
    int gameCorrectGuesses_inTotal;
    int bothPlayersCorrectGuess;

    int firstGuessIndexP1, secondGuessIndexP1;
    string firstCardNameP1, secondCardNameP1;

    int firstGuessIndexP2, secondGuessIndexP2;
    string firstCardNameP2, secondCardNameP2;

    [SerializeField] TextMeshProUGUI playerTurnText;

    [SerializeField] TextMeshProUGUI scoreTextP1;
    [SerializeField] TextMeshProUGUI scoreTextP2;

    public int countCorrectGuessesP1 = 0;
    public int countCorrectGuessesP2 = 0;

    [SerializeField] AudioSource cardFlip;

    public bool isGameComplete = false;

    void Awake()
    {
        cards = Resources.LoadAll<Sprite>("CardSprites/");
    }

    void Start()
    {
        GetButtons();
        AddCardPuzzles();
        Shuffle(gamePuzzlesP1);
        Player2Puzzle();
        gameCorrectGuesses_inTotal = (gamePuzzlesP1.Count/2);
    }

    void Update()
    {
        if (isPlayer1Active)
        {
            if (Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                PickACardP1();
            }
        }
        if (isPlayer2Active)
        {
            if (Input.GetMouseButtonDown(0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                PickACardP2();
            }
        }
    }    

    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("cardButton");

        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = btnBgImage;
        }
    }

    void AddCardPuzzles()      //ActiveP1 and ActiveP2
    {
        int cardCount = btns.Count;
        int index = 0;

        for (int i = 0; i < cardCount; i++)
        {
            if (index == cardCount / 2)
            {
                index = 0;
            }
            gamePuzzlesP1.Add(cards[index]);
            index++;
        }
    }

    public void PickACardP1()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if (!firstGuessP1)
        {
            firstGuessP1 = true;
            firstGuessIndexP1 = int.Parse(name);
            firstCardNameP1 = gamePuzzlesP1[firstGuessIndexP1].name;
            btns[firstGuessIndexP1].image.sprite = gamePuzzlesP1[firstGuessIndexP1];
        }
        else if (!secondGuessP1)
        {
            secondGuessIndexP1 = int.Parse(name);
            secondCardNameP1 = gamePuzzlesP1[secondGuessIndexP1].name;
            btns[secondGuessIndexP1].image.sprite = gamePuzzlesP1[secondGuessIndexP1];
            countGuesses++;
            StartCoroutine(CheckIfCardMatchP1());
        }
    }

    IEnumerator CheckIfCardMatchP1()
    {
        yield return new WaitForSeconds(1f);
        if (firstCardNameP1 == secondCardNameP1)
        {
            yield return new WaitForSeconds(0.5f);

            btns[firstGuessIndexP1].interactable = false;
            btns[secondGuessIndexP1].interactable = false;

            btns[firstGuessIndexP1].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndexP1].image.color = new Color(0, 0, 0, 0);
            ScoreP1();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            btns[firstGuessIndexP1].image.sprite = btnBgImage;
            btns[secondGuessIndexP1].image.sprite = btnBgImage;
            isPlayer2Active = true;
            isPlayer1Active = false;
            playerTurnText.text = "Player 2's Turn";
        }
        yield return new WaitForSeconds(0.5f);
        firstGuessP1 = secondGuessP1 = false;
    }

   void ScoreP1()
    {
        countCorrectGuessesP1 += 1;
        scoreTextP1.text = "Player1 : " + countCorrectGuessesP1.ToString();
        GameFinish();
    }

    void Shuffle(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void Player2Puzzle()
    {
        gamePuzzlesP2 = gamePuzzlesP1;
    }

    public void PickACardP2()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        if (!firstGuessP2)
        {
            firstGuessP2 = true;
            firstGuessIndexP2 = int.Parse(name);
            firstCardNameP2 = gamePuzzlesP2[firstGuessIndexP2].name;
            btns[firstGuessIndexP2].image.sprite = gamePuzzlesP2[firstGuessIndexP2];
        }
        else if (!secondGuessP2)
        {
           
            secondGuessIndexP2 = int.Parse(name);
            secondCardNameP2 = gamePuzzlesP2[secondGuessIndexP2].name;
            btns[secondGuessIndexP2].image.sprite = gamePuzzlesP2[secondGuessIndexP2];
            countGuesses++;
            StartCoroutine(CheckIfCardMatchP2());
        }
    }

    IEnumerator CheckIfCardMatchP2()
    {
        yield return new WaitForSeconds(1f);
        if (firstCardNameP2 == secondCardNameP2)
        {
            yield return new WaitForSeconds(0.5f);

            btns[firstGuessIndexP2].interactable = false;
            btns[secondGuessIndexP2].interactable = false;

            btns[firstGuessIndexP2].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndexP2].image.color = new Color(0, 0, 0, 0);
            ScoreP2();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            btns[firstGuessIndexP2].image.sprite = btnBgImage;
            btns[secondGuessIndexP2].image.sprite = btnBgImage;
            isPlayer1Active = true;
            isPlayer2Active = false;
            playerTurnText.text = "Player 1's Turn";
        }
        yield return new WaitForSeconds(0.5f);
        firstGuessP2 = secondGuessP2 = false;
    }

    void ScoreP2()
    {
        countCorrectGuessesP2 += 1;
        scoreTextP2.text = "Player2 : " + countCorrectGuessesP2.ToString();
        GameFinish();
    }

    void GameFinish()
    {
        bothPlayersCorrectGuess = countCorrectGuessesP1 + countCorrectGuessesP2;
        if (bothPlayersCorrectGuess == gameCorrectGuesses_inTotal)
        {
            isGameComplete = true;
        }
    }
}
