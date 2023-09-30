using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public int value = 0;
    public int numCard;
    public CardManager cardManager;
    
    public void OnClicked(Button button)
    {
        if (cardManager.coroutineOver)
        {
            if (!cardManager.hasBeenClicked)
            {
                cardManager.savedCard = button.GetComponent<CardController>().value;
                cardManager.flipFirstCard(numCard);
                cardManager.hasBeenClicked = true;
            }
            else
            {
                cardManager.finalCard = button.GetComponent<CardController>().value;
                cardManager.flipSecondCard(numCard);

                if (cardManager.savedCard == value)
                {
                    Debug.Log("match");

                    cardManager.matchesMade++;
                }
            }
        }
    }
}
