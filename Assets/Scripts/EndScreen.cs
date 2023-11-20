using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] TextMeshProUGUI scoreBoard;
    CardController cardControllerSc;

    private void Awake()
    {
        cardControllerSc = FindObjectOfType<CardController>();
    }

    public void ScoreCalculator()
    {
        scoreBoard.text = "PLAYER1 SCORE = " + cardControllerSc.countCorrectGuessesP1 + " & PLAYER2 SCORE = " + cardControllerSc.countCorrectGuessesP2; 
        if (cardControllerSc.countCorrectGuessesP1 > cardControllerSc.countCorrectGuessesP2)
        {
            message.text = "PLAYER1 IS THE WINNER.";
        }
        else if (cardControllerSc.countCorrectGuessesP1 < cardControllerSc.countCorrectGuessesP2)
        {
            message.text = "PLAYER2 IS THE WINNER.";
        }
        else
        {
            message.text = "It's a DRAW";
        }
    }
}
