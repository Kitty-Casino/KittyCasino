using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public int value = 0;
    public CardManager cardManager;

    public void OnClicked(Button button)
    {
        if (!cardManager.hasBeenClicked)
        {
            cardManager.savedCard = button.GetComponent<CardController>().value;
            cardManager.hasBeenClicked = true;
        }
        else
        {
            if (cardManager.savedCard == value)
            {
                Debug.Log("win");
            }
            else
            {
                Debug.Log("lose");
            }
        }
    }
}
