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
    public Sprite[] cardDecor;
    
    public void Clicked()
    {
        if (cardManager.coroutineOver)
        {
            if (!cardManager.hasBeenClicked)
            {
                cardManager.savedCard = value;
                cardManager.savedCardNum = numCard;
                cardManager.flipFirstCard(numCard);
                cardManager.hasBeenClicked = true;
            }
            else if (cardManager.savedCardNum != numCard)
            {
                cardManager.finalCard = value;
                cardManager.finalCardNum = numCard;
                cardManager.flipSecondCard(numCard);

                if (cardManager.savedCard == value)
                {
                    cardManager.matchesMade++;
                }
            }
        }
    }
}
