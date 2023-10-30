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
    public CoinsController coinsController;
    // public TextMeshProUGUI betButtonText;

    [Header("UI")]
    public GameObject placeBetUI;
    public GameObject raiseBetUI;
    public GameObject winScreen;
    public GameObject loseScreen;

    [Header("Winning")]
    private GameObject[] playerHand;
    private GameObject[] dealerHand;
    public int replayAmount;

    void Start()
    {
        coinsController = GameObject.Find("CoinsController").GetComponent<CoinsController>();
        replayAmount = 0;
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        bettingUI.SetActive(false);
        checkButton.SetActive(false);
        raiseBetUI.SetActive(false);
        betButton.onClick.AddListener(BetMade);
        Initialize();
    }

    public void Initialize()
    {
        if (replayAmount > 0)
        {
            winScreen.SetActive(false);
            loseScreen.SetActive(false);
            bettingUI.SetActive(true);
            placeBetUI.SetActive(true);
            raiseBetUI.SetActive(false);
            betButton.onClick.AddListener(BetMade);
            StopAllCoroutines();
            betSlider.value = 0;
            betValue = 0;
            betSlider.maxValue = coinsController.totalCoins;

            for (int i = 0; i < 5; i++)
            {
                communityCards[i].GetComponent<Image>().sprite = communityCards[i].GetComponent<PokerCardController>().cardSides[0];
            }

            playerCards[0].GetComponent<Image>().sprite = playerCards[0].GetComponent<PokerCardController>().cardSides[0];
            playerCards[1].GetComponent<Image>().sprite = playerCards[1].GetComponent<PokerCardController>().cardSides[0];
            dealerCards[0].GetComponent<Image>().sprite = playerCards[0].GetComponent<PokerCardController>().cardSides[0];
            dealerCards[1].GetComponent<Image>().sprite = playerCards[1].GetComponent<PokerCardController>().cardSides[0];
        }
        
        playerHand = new GameObject[7];
        dealerHand = new GameObject[7];
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
        yield return new WaitForSeconds(5);
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
            //  betText.text = "RAISE YOUR BET?";
            // betButtonText.text = "Raise Bet";
            placeBetUI.SetActive(false);
            raiseBetUI.SetActive(true);
            betSlider.maxValue = coinsController.totalCoins;
        }      
        else
        {
            bettingUI.SetActive(true);
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

        Debug.Log("playerHand: " + playerHandValue);
        Debug.Log("dealerHand: " + dealerHandValue);

        if (playerHandValue > dealerHandValue)
        {
            winScreen.SetActive(true);
            coinsController.totalCoins += betValue;
            coinsController.coinsText.text = "" + coinsController.totalCoins;
        }
        else if (playerHandValue < dealerHandValue)
        {
            loseScreen.SetActive(true);
            coinsController.totalCoins -= betValue;
            coinsController.coinsText.text = "" + coinsController.totalCoins;
        }
        else // equivalency - player wins
        {
            Debug.Log("tie");
            winScreen.SetActive(true);
            coinsController.totalCoins += betValue;
            coinsController.coinsText.text = "" + coinsController.totalCoins;
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
        List<int> existingValues = new List<int>();
        int[] usedValues = new int[15]; 
        int[] valuesSorted = (int[])values.Clone();
        Array.Sort(valuesSorted);

        for (int i = 0; i < valuesSorted.Length - 1; i++)
        {
            if (!existingValues.Contains(valuesSorted[i]))
            {
                existingValues.Add(valuesSorted[i]);
                usedValues[valuesSorted[i]]++;
            }
            else
            {
                usedValues[valuesSorted[i]]++;
            }

            if (valuesSorted[i] == valuesSorted[i + 1])
            {
                numMatching++;
                matchedValue = valuesSorted[i];
            }
        }

        bool isPair = false;
        bool isThreeOfKind = false;

        // check for pair
        if (numMatching == 1)
        {
            Debug.Log("pair");
            isPair = true;
            handValue += 20; // bonus for getting pair
            handValue += matchedValue; // in case of 2 players getting pair, card value dictates winner
        }
        // check for 2 pair
        else if (numMatching == 2)
        {
            Debug.Log("two pair");
            handValue += 30;
            handValue += matchedValue;
        }

        for (int i = 0; i < usedValues.Length; i++)
        {
            // check for 3 of a kind
            if (usedValues[i] == 3)
            {
                Debug.Log("3 of a kind");
                isThreeOfKind = true;
                handValue += 40;
                handValue += matchedValue; // 3 of a kind inherently contains a pair
            }

            if (usedValues[i] == 4)
            {
                Debug.Log("4 of a kind");
                handValue += 80;
                handValue += matchedValue;
            }
        }

        // check for full house ------------------------------------- is broken
        if (isThreeOfKind && isPair) // i believe this disregards the chance that the pair is in fact also the 3 of a kind
        {
            Debug.Log("full house");
            handValue += 70;
            handValue += matchedValue;
        }

        // check for flush
        List<string> existingSuits = new List<string>();
        int[] usedSuits = new int[4];

        for (int i = 0; i < suits.Length; i++)
        {
            if (!existingSuits.Contains(suits[i]))
            {
                existingSuits.Add(suits[i]);

                // there is absolutely a better way to do this
                if (suits[i] == "Hearts") // hearts is assigned to index 0
                {
                    usedSuits[0]++;
                }
                else if (suits[i] == "Diamonds") // diamonds is assigned to index 1
                {
                    usedSuits[1]++;
                }
                else if (suits[i] == "Spades") // spades is assigned to index 2
                {
                    usedSuits[2]++;
                }
                else // clubs is assigned to index 3
                {
                    usedSuits[3]++;
                }
            }
            else
            {
                // there is absolutely a better way to do this
                if (suits[i] == "Hearts") // hearts is assigned to index 0
                {
                    usedSuits[0]++;
                }
                else if (suits[i] == "Diamonds") // diamonds is assigned to index 1
                {
                    usedSuits[1]++;
                }
                else if (suits[i] == "Spades") // spades is assigned to index 2
                {
                    usedSuits[2]++;
                }
                else // clubs is assigned to index 3
                {
                    usedSuits[3]++;
                }
            }
        }

        bool isFlush = false;
        for (int i = 0; i < usedSuits.Length; i++)
        {
            // check for flush
            if (usedSuits[i] >= 5)
            {
                Debug.Log("flush");
                isFlush = true;
                handValue += 60;
                // -------------------- IMPLEMENT CHECK FOR HIGH CARD ------------------------
            }
        }

        // checks for royals
        int numRoyal = 0;
        for (int i = 0; i < existingValues.Count; i++)
        {
            if (existingValues[i] >= 10)
            {
                numRoyal++;
            }
        }

        // check for royal flush
        if (numRoyal >= 5 && isFlush)
        {
            Debug.Log("royal flush");
            handValue += 100;
            // -------------------- IMPLEMENT CHECK FOR HIGH CARD ------------------------
        }

        // check for straight -------------- DOES NOT ACCOUNT FOR ACE BEING LOW !!!!!
        int numContiguous = 0;
        
        for (int i = 0; i < usedValues.Length; i++) // broken
        {
            Debug.Log("usedvalues[" + i + "]: " + usedValues[i]);

            if (usedValues[i] > 0)
            {
                int index = i;
                numContiguous = 0;

                while (usedValues[index] > 0)
                {
                    numContiguous++;
                    // Debug.Log("num contiguous: " + numContiguous);           <-- seems to work

                    if (index <= usedValues.Length - 1)
                    {
                        index++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        Debug.Log(numContiguous);

        if (numContiguous >= 5 && !isFlush)
        {
            Debug.Log("straight");
            handValue += 50;
            // -------------------- IMPLEMENT CHECK FOR HIGH CARD ------------------------
        }
        // check for straight flush
        else if (numContiguous >= 5 && isFlush)
        {
            Debug.Log("straight flush");
            handValue += 90;
            // -------------------- IMPLEMENT CHECK FOR HIGH CARD ------------------------
        }

        return handValue;
    }
}
