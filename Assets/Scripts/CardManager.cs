using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] cards;
    private int[] numValues;
    public int savedCard;
    public bool hasBeenClicked = false;

    void Start()
    {
        numValues = new int[8];
        
        for (int i = 0; i < numValues.Length; i += 2)
        {
            numValues[i] = i;
            numValues[i + 1] = i;
        }

        shuffleCards(numValues);

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].GetComponent<CardController>().value = numValues[i];
        }
    }

    private void shuffleCards(int[] numValues)
    {
        for (int i = 0; i < numValues.Length - 1; i++)
        {
            int value = Random.Range(i, numValues.Length - 1);
            int temp = numValues[i];
            numValues[i] = numValues[value];
            numValues[value] = temp;
        }
    }
}