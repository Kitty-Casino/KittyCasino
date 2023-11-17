using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackjackPlayerScript : MonoBehaviour
{
    // Script is used on both player and dealer, tracks both of their cards in an array
    
    // Refs to deck and card scripts
    public CardScript cardScript;
    public DeckScript deckScript;

    // Total value of the player/dealers hand
    public int handValue = 0;

    //Array of card objects on the board
    public GameObject[] hand;

    //Index of next card to be turned over
    public int cardIndex = 0;

    // Used to track aces for setting their value to 1 or 11 approrpiately 
    List<CardScript> aceList = new List<CardScript>();
  
    public void StartHand()
    {
        GetCard();
        GetCard();
    }

    // Add a hand to the player/dealer hand
    public int GetCard()
    {
        // Gets a card and uses DealCard to assign correct sprite and value to the card on the board
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        // Show card on screen
        hand[cardIndex].GetComponent<Image>().enabled = true;
        // Add card value to running total of the hand
        handValue += cardValue;
        // If value is 1, it's an ace go figure
        if (cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }
        // Decides if 11 should be used instead of 1 for an Ace
        AceCheck();
        cardIndex++;
            return handValue;
    }
    // Checks for aces and decides if it should come out to be 11 or 1 depending on player/dealers current hand
    public void AceCheck()
    {
        foreach (CardScript ace in aceList)
        {
            if (handValue + 10 < 22 && ace.GetValueOfCard() == 1)
            {
                Debug.Log("Ace value set to 11");
                ace.SetValue(11);
                handValue += 10;
            }
            else if (handValue > 21 && ace.GetValueOfCard() == 11)
            {
                Debug.Log("Ace value set to 1");
                ace.SetValue(1);
                handValue -= 10;
            }
        }
    }

    // Resets dealers and players hand
    public void ResetHand()
    {
        for (int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Image>().enabled = false;
        }
        cardIndex = 0;
        handValue = 0;
        aceList = new List<CardScript>();
    }
}
