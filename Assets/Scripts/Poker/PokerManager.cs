using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokerManager : MonoBehaviour
{
    [Header("Cards")]
    public List<Sprite> cardValues; // length 52
    // public int[] used; // tracks cards that have been dealt already
    public GameObject[] communityCards; // will always be length 5
    public GameObject[] dealerCards; // will always be length 5
    public GameObject[] playerCards; // will always be length 5

    [Header("Betting")]
    public GameObject bettingUI;
    public Button betButton;
    public Slider betSlider;
    public bool betMade;
    public int betValue;

    void Start()
    {
        bettingUI.SetActive(false);
        betButton.onClick.AddListener(BetMade);
        Initialize();
    }

    void Update()
    {

    }

    private void Initialize()
    {
        AssignCards();
        StartCoroutine(ShowPlayerCards());
    }

    private void AssignCards()
    {
        for (int i = 0; i < (dealerCards.Length * 2); i++)
        {
            int randomCard = Random.Range(0, cardValues.Count);

            if (i % 2 == 0) // assign cards one by one, player is dealt first
            {
                playerCards[i % playerCards.Length].GetComponent<PokerCardController>().cardSides[1] = cardValues[randomCard];
                cardValues.RemoveAt(randomCard);
            }
            else if (i % 2 == 1)
            {
                dealerCards[i % dealerCards.Length].GetComponent<PokerCardController>().cardSides[1] = cardValues[randomCard];
                cardValues.RemoveAt(randomCard);
            }
        }

        for (int i = 0; i < communityCards.Length; i++)
        {
            int randomCard = Random.Range(0, cardValues.Count);
            communityCards[i].GetComponent<PokerCardController>().cardSides[1] = cardValues[randomCard];
            cardValues.RemoveAt(randomCard);
        }
    }

    // shows player first 2 cards to initiate bet
    IEnumerator ShowPlayerCards()
    {
        playerCards[0].GetComponent<Image>().sprite = playerCards[0].GetComponent<PokerCardController>().cardSides[1];
        playerCards[1].GetComponent<Image>().sprite = playerCards[1].GetComponent<PokerCardController>().cardSides[1];
        yield return new WaitForSeconds(5);
        StartCoroutine(BettingScreen());
    }

    // exposes betting UI and holds rest of game until bet is made
    // triggers card flip once bet has been made
    IEnumerator BettingScreen()
    {
        bettingUI.SetActive(true);

        while (!betMade)
        {
            yield return null;
        }

        bettingUI.SetActive(false);
    }

    public void BetMade()
    {
        betMade = true;
        betValue = (int)betSlider.value;

        // if (!decremented)
        // {
        //     decremented = true;
        //     coinController.DecrementCoins((int) betSlider.value);
        // }
    }
}
