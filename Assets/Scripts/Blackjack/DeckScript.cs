using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public Sprite[] cardSprites;
    int[] cardValues = new int[53];
    int currentIndex = 0;
    
    void Start()
    {
        GetCardValues();
    }

    // Assigns values to the cards
    public void GetCardValues()
    {
        int num = 0;
        // Counts up to the amount of cards
        for(int i = 0; i <cardSprites.Length; i++)
        {
            num = i;
            num %= 13;
            // If there is a remainder after num / 13, then remainder is used as the value, if it's over 10, then it uses 10 as the value
            if (num > 10 || num == 0)
            {
                num = 10;
            }
            cardValues[i] = num++;
        }
        
    }


    public void Shuffle()
    {
        // Randomizes the deck through Random.Range
        for(int i = cardSprites.Length - 1; i > 0; --i)
        {
            int j = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * (cardSprites.Length - 1)) + 1;
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[j];
            cardSprites[j] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[j];
            cardValues[j] = value;
        }
        currentIndex = 1;
    }

    public int DealCard(CardScript cardScript)
    {
        // Sets players/dealers cards and assigns values based on the index
        cardScript.SetSprite(cardSprites[currentIndex]);
        cardScript.SetValue(cardValues[currentIndex]);
        currentIndex++;
        return cardScript.GetValueOfCard();
    }

    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }
}
