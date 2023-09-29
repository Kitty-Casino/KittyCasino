using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour
{
    public GameObject[] cards;
    public GameObject[] cardDetails;
    private int[] numValues;
    public int savedCard;
    public bool hasBeenClicked = false;

    void Start()
    {
        cards = GameObject.FindGameObjectsWithTag("Cards");
        cardDetails = GameObject.FindGameObjectsWithTag("Card Details");
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

        StartCoroutine(flipCards());
    }

    // flips 3 random cards with different values
    // <param> array of GameObjects of all the cards
    private void showRandomCards(GameObject[] cards)
    {
        int rand1 = Random.Range(0, 8);
        int temp = Random.Range(0, 8);
        int tempValue = cards[rand1].GetComponent<CardController>().value;
        int rand2 = 0;
        int rand3 = 0;
        bool flag = false;

        if (temp != rand1 &&
            cards[temp].GetComponent<CardController>().value != tempValue)
        {
            rand2 = temp;
        }
        else if (temp > 8)
        {
            rand2 = temp++;
        }
        else if (temp > 0)
        {
            rand2 = temp--;
        }

        while (!flag)
        {
            temp = Random.Range(0, 8);

            if (temp != rand2 && temp != rand1 &&
            cards[temp].GetComponent<CardController>().value != cards[rand2].GetComponent<CardController>().value &&
            cards[temp].GetComponent<CardController>().value != cards[rand1].GetComponent<CardController>().value)
            {
                rand3 = temp;
                flag = true;
            }
        }

        // 
        for (int i = 0; i < cards.Length; i++)
        {
            if (i == rand1 || i == rand2 || i == rand3)
            {
                // theres def a better method for this but since we have no designs yet this is what it is
                cardDetails[i].GetComponent<TMP_Text>().text = "" + cards[i].GetComponent<CardController>().value;
            }
        }
    }

    // shuffles the values of the cards
    // <param> int array of the values to be assigned
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

    // shows the values of the cards for 5 seconds 
    IEnumerator flipCards()
    {
        showRandomCards(cards);
        yield return new WaitForSeconds(5);

        for (int i = 0; i < cards.Length; i++)
        {
            // again there is def a better method
            cardDetails[i].GetComponent<TMP_Text>().text = "";
        }
    }
}