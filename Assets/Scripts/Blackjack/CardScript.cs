using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardScript : MonoBehaviour
{
    // This script handles the values and proper sprites for all of the cards on the board, generally just a data retainer for all cards
    // Value of cards assigned here
    public int value = 0;
    public Image cardImage;

    private void Start()
    {
        ResetCard();
    }
    public int GetValueOfCard()
    {
        return value;
    }

    public void SetValue(int newValue)
    {
        value = newValue;
    }

    public void SetSprite(Image newSprite)
    {
        cardImage.sprite = newSprite.sprite;
    }

    public string GetSpriteName()
    {
        return cardImage.sprite.name;
    }

    public void ResetCard()
    {
        Sprite back = GameObject.Find("Deck").GetComponent<DeckScript>().GetCardBack();
        cardImage.sprite = back;
        value = 0;
    }
}
