using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Ref to CoinsController for money management
    CoinsController coinsController = CoinsController.Instance;

    // Buttons for game
    public Button dealButton;
    public Button hitButton;
    public Button standButton;
    public Button betButton;
    
    private int standClicks = 0;

    // Refs to player and dealers script/hand
    public BlackjackPlayerScript playerScript;
    public BlackjackPlayerScript dealerScript;

    // HUD Text for scores and bets
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI dealerScoreText;
    public TextMeshProUGUI betText;
    public TextMeshProUGUI standText;
    public TextMeshProUGUI mainText;
    // public Text mainText;

    // Card hiding dealer's 2nd card
    public GameObject hideCard;
    public GameObject winPanel;
    public GameObject losePanel;

    // Bet amount total
    int pot = 0;
    void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        mainText.gameObject.SetActive(false);
        dealButton.onClick.AddListener(() => DealClicked());
        hitButton.onClick.AddListener(() => HitClicked());
        standButton.onClick.AddListener(() => StandClicked());
        betButton.onClick.AddListener(() => BetClicked());
    }

    private void DealClicked()
    {
        playerScript.ResetHand();
        dealerScript.ResetHand();
        dealerScoreText.gameObject.SetActive(false);
        mainText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().Shuffle();

        playerScript.StartHand();
        dealerScript.StartHand();

        scoreText.text = "Your Hand: \n" + playerScript.handValue.ToString();
        dealerScoreText.text = "Dealers Hand: " + playerScript.handValue.ToString();
        hideCard.GetComponent<Renderer>().enabled = true;

        dealButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
        standText.text = "Stand";

 
        betText.text = "Your Bet: \n" + "$" + pot.ToString();
      
    }

    private void HitClicked()
    {
        // Checks if there is still room on the board
        if(playerScript.cardIndex <= 10)
        {
            playerScript.GetCard();
            scoreText.text = "Your Hand: " + playerScript.handValue.ToString();
            if (playerScript.handValue > 20) RoundOver();
        }
    }

    private void StandClicked()
    {
        standClicks++;
        if (standClicks > 1) RoundOver();
        HitDealer();
        standText.text = "Call";
    }

    private void HitDealer()
    {
        while (dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();
            dealerScoreText.text = "Dealer's Hand: " + dealerScript.handValue.ToString();
            if (dealerScript.handValue > 20) RoundOver();
        }
    }

    public void BetClicked()
    {
        
        int intBet = 20; 
       
        coinsController.DecrementCoins(intBet);
        pot += (intBet * 2);
        betText.text = "Your Bet: \n" + "$" + pot.ToString();
    }

    void RoundOver()
    {
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;

        if (standClicks < 2 && !playerBust && !dealerBust && !player21 && !dealer21) return;

        bool roundOver = true;
        // Both sides bust, push happens
        if (playerBust && dealerBust)
        {
            mainText.text = "All Bust: Bets Returned";
            mainText.gameObject.SetActive(true);
            coinsController.IncrementCoins(pot / 2);
        }
        // If player bust but dealer didn't, or dealer has more points, dealer wins
        else if (playerBust || (!dealerBust && dealerScript.handValue > playerScript.handValue))
        {
            losePanel.SetActive(true);
        }
        // If dealer bust, player didn't, or player has more points, player wins
        else if (dealerBust || playerScript.handValue > dealerScript.handValue)
        {
            winPanel.SetActive(true);
            coinsController.IncrementCoins(pot);
        }
        // Tie check, return bets
        else if (playerScript.handValue == dealerScript.handValue)
        {
            mainText.text = "Push: Bets Returned";
            mainText.gameObject.SetActive(true);
            coinsController.IncrementCoins(pot / 2);
        }
        else
        {
            roundOver = false;
        }

        if (roundOver)
        {
            hitButton.gameObject.SetActive(false);
            standButton.gameObject.SetActive(false);
            dealButton.gameObject.SetActive(true);
            dealerScoreText.gameObject.SetActive(true);
            hideCard.GetComponent<Renderer>().enabled = false;
            standClicks = 0;
            pot = 0;
            betText.text = "Your Bet: \n" + "$" + pot.ToString();
        }
    }

    public void PromptHide()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
}
