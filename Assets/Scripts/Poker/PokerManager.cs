using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokerManager : MonoBehaviour
{
    [Header("Cards")]
    public Sprite[] cardValues; // currently length 13 bc only have 1 suit
    public int[] usedValues;
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
        usedValues = new int[13]; // should be same as total # of cards

        for (int i = 0; i < cardValues.Length; i++)
        {
            
        }
    }

    // shows player first 2 cards to initiate bet
    IEnumerator ShowPlayerCards()
    {
        // flip cards
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
        betValue = (int) betSlider.value;

        // if (!decremented)
        // {
        //     decremented = true;
        //     coinController.DecrementCoins((int) betSlider.value);
        // }
    }
}
