using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    void Start()
    {
        bettingUI.SetActive(false);
        checkButton.SetActive(false);
        betButton.onClick.AddListener(BetMade);
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
            int randomCard = Random.Range(0, cardValues.Count);

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
            int randomCard = Random.Range(0, cardValues.Count);
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
