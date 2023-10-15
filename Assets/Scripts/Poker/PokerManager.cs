using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class PokerManager : MonoBehaviour
{
    [Header("Cards")]
    public List<Sprite> cardValues; // length 52
    public GameObject[] communityCards; // will always be length 5
    public GameObject[] dealerCards; // will always be length 2
    public GameObject[] playerCards; // will always be length 2

    [Header("Betting")]
    public GameObject bettingUI;
    public Button betButton;
    public Slider betSlider;
    public bool betMade;
    public int betValue;
    public int betNum;
    public TextMeshProUGUI betText;
    public GameObject checkButton;
    public TextMeshProUGUI betUIText;
    public TextMeshProUGUI betButtonText;

    [Header("Winning")]
    private GameObject[] playerHand;
    private GameObject[] dealerHand;

    void Start()
    {
        bettingUI.SetActive(false);
        checkButton.SetActive(false);
        betButton.onClick.AddListener(BetMade);
        playerHand = new GameObject[7];
        dealerHand = new GameObject[7];
        Initialize();
    }

    private void Initialize()
    {
        betNum = 0;
        AssignCards();
        StartCoroutine(BettingScreen());
    }

    private void AssignCards()
    {
        int index1 = 0;
        int index2 = 0;

        for (int i = 0; i < (dealerCards.Length * 2); i++)
        {
            int randomCard = UnityEngine.Random.Range(0, cardValues.Count);

            if (i % 2 == 0) // assign cards one by one, player is dealt first
            {
                playerCards[index1].GetComponent<PokerCardController>().cardSides[1] = cardValues[randomCard];
                cardValues.RemoveAt(randomCard);
                index1++;
            }
            else if (i % 2 == 1)
            {
                dealerCards[index2].GetComponent<PokerCardController>().cardSides[1] = cardValues[randomCard];
                cardValues.RemoveAt(randomCard);
                index2++;
            }
        }

        for (int i = 0; i < communityCards.Length; i++)
        {
            int randomCard = UnityEngine.Random.Range(0, cardValues.Count);
            communityCards[i].GetComponent<PokerCardController>().cardSides[1] = cardValues[randomCard];
            cardValues.RemoveAt(randomCard);
        }
    }

    private void DetermineWinner()
    {
        // populate array
        for (int i = 0; i < 2; i++)
        {
            playerHand[i] = playerCards[i];
            dealerHand[i] = dealerCards[i];
        }

        for (int i = 2; i < 7; i++)
        {
            playerHand[i] = communityCards[(i - 2)];
            dealerHand[i] = communityCards[(i - 2)];
        }

        int playerHandValue = GetHandValue(playerHand);
        int dealerHandValue = GetHandValue(dealerHand);

        if (playerHandValue > dealerHandValue) 
        {
            // player win
        }
        else if (playerHandValue < dealerHandValue)
        {
            // dealer win
        }
        else // equivalency
        {
            // win half
        }
    }

    private int GetHandValue(GameObject[] hand)
    {
        int handValue = 0;
        string[] suits = new string[hand.Length]; // DO NOT SORT !!!!
        int[] values = new int[hand.Length]; // DO NOT SORT !!!!

        for (int i = 0; i < hand.Length; i++)
        {
            string suitName = hand[i].GetComponent<Image>().sprite.name;
            string[] parts = suitName.Split(new string[] { "_" }, StringSplitOptions.None);

            if (parts[0] == "Jack")
            {
                values[i] = 11;
            }
            else if (parts[0] == "Queen")
            {
                values[i] = 12;
            }
            else if (parts[0] == "King")
            {
                values[i] = 13;
            }
            else if (parts[0] == "Ace")
            {
                values[i] = 14;
            }
            else
            {
                values[i] = int.Parse(parts[0]);
            }
            
            suits[i] = parts[1];
        }

        // check for high card
        int max = 0;

        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] > max)
            {
                max = values[i];
            }
        }

        handValue += max; // works properly

        // checks for amount of matching values
        int numMatching = 0;
        int matchedValue = 0;
        int[] valuesSorted = (int[]) values.Clone();
        Array.Sort(valuesSorted);

        for (int i = 0; i < valuesSorted.Length - 1; i += 2)
        {
            if (valuesSorted[i] == valuesSorted[i + 1])
            {
                numMatching++;
                matchedValue = valuesSorted[i];
            }
        }

        // check for four of a kind
        if (numMatching == 4)
        {
            Debug.Log("4 of a kind");
            handValue += 50; // bonus for getting 4 of a kind
            handValue += matchedValue; // in case of 2 players getting 4 of a kinds, card value dictates winner
        }
        // check 3 of a kind
        else if (numMatching == 3)
        {
            Debug.Log("3 of a kind");
            handValue += 35; // bonus for getting 3 of a kind
            handValue += matchedValue; // in case of 2 players getting 3 of a kinds, card value dictates winner
        }
        // check for pair 
        else if (numMatching == 2)
        {
            Debug.Log("pair");
            handValue += 20; // bonus for getting pair
            handValue += matchedValue; // in case of 2 players getting pair, card value dictates winner
        }

        // check for 2 pair

        // check for straight

        // check for flush

        // check for full house

        // check for straight flush

        // check for royal flush

        return handValue;
    }

    // shows player cards to initiate bet
    private void ShowPlayerCards()
    {
        playerCards[0].GetComponent<Image>().sprite = playerCards[0].GetComponent<PokerCardController>().cardSides[1];
        playerCards[1].GetComponent<Image>().sprite = playerCards[1].GetComponent<PokerCardController>().cardSides[1];
        StartCoroutine(ViewCards());
    }

    private void ShowFlop()
    {
        for (int i = 0; i < 3; i++)
        {
            communityCards[i].GetComponent<Image>().sprite = communityCards[i].GetComponent<PokerCardController>().cardSides[1];
        }

        StartCoroutine(ViewCards());
    }

    private void ShowTurn()
    {
        communityCards[3].GetComponent<Image>().sprite = communityCards[3].GetComponent<PokerCardController>().cardSides[1];
        StartCoroutine(ViewCards());
    }

    private void ShowRiver()
    {
        communityCards[4].GetComponent<Image>().sprite = communityCards[4].GetComponent<PokerCardController>().cardSides[1];
        StartCoroutine(SeeDealer());
    }

    IEnumerator SeeDealer()
    {
        yield return new WaitForSeconds(3);
        dealerCards[0].GetComponent<Image>().sprite = dealerCards[0].GetComponent<PokerCardController>().cardSides[1];
        dealerCards[1].GetComponent<Image>().sprite = dealerCards[1].GetComponent<PokerCardController>().cardSides[1];
        DetermineWinner();
    }

    IEnumerator ViewCards()
    {
        yield return new WaitForSeconds(5);
        betMade = false;
        StartCoroutine(BettingScreen());
    }

    // exposes betting UI and holds rest of game until bet is made
    // triggers card flip once bet has been made
    IEnumerator BettingScreen()
    {
        if (betNum != 0)
        {
            checkButton.SetActive(true);
        }

        bettingUI.SetActive(true);

        if (betNum > 0)
        {
            betText.text = "RAISE YOUR BET?";
            betButtonText.text = "Raise Bet";
        }        

        while (!betMade)
        {
            yield return null;
        }

        bettingUI.SetActive(false);
        checkButton.SetActive(false);

        switch (betNum)
        {
            case 0:
                ShowPlayerCards();
                break;
            case 1:
                ShowFlop();
                break;
            case 2:
                ShowTurn();
                break;
            case 3:
                ShowRiver();
                break;
        }
        
        betNum++;
    }

    public void BetMade()
    {
        betMade = true;
        betValue = (int) betSlider.value;
        betUIText.text = "" + betValue;

        // if (!decremented)
        // {
        //     decremented = true;
        //     coinController.DecrementCoins((int) betSlider.value);
        // }
    }

    public void CheckButton()
    {
        switch (betNum)
        {
            case 0:
                ShowPlayerCards();
                break;
            case 1:
                ShowFlop();
                break;
            case 2:
                ShowTurn();
                break;
            case 3:
                ShowRiver();
                break;
        }

        betNum++;
        bettingUI.SetActive(false);
    }
}
