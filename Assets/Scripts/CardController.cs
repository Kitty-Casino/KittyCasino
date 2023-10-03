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
        // Debug.Log("Card " + numCard + " Clicked");
        if (cardManager.coroutineOver)
        {
            if (!cardManager.hasBeenClicked)
            {
                cardManager.savedCard = value;
                // Debug.Log("savedCard: " + value);
                cardManager.savedCardNum = numCard;
                // Debug.Log("savedCardNum: " + numCard);
                cardManager.flipFirstCard(numCard);
                // Debug.Log("Card " + numCard + " Flipped");
                cardManager.hasBeenClicked = true;
            }
            else if (cardManager.savedCardNum != numCard)
            {
                cardManager.finalCard = value;
                cardManager.finalCardNum = numCard;
                cardManager.flipSecondCard(numCard);
                // Debug.Log("Card " + numCard + " Flipped");

                if (cardManager.savedCard == value)
                {
                    // Debug.Log("match");

                    cardManager.matchesMade++;
                }
            }
        }
    }
}
