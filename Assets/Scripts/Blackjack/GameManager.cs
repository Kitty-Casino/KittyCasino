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
    public Button doubleButton;
    public Button splitButton;
    
    private int standClicks = 0;

    // Refs to player and dealers script/hand
    public BlackjackPlayerScript playerScript;
    public BlackjackPlayerScript dealerScript;
    public BlackjackPlayerScript splitScript;

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
        doubleButton.onClick.AddListener(() => DoubleClicked());

        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
        doubleButton.gameObject.SetActive(false);
        splitButton.gameObject.SetActive(false);
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
        dealerScoreText.text = "Dealers Hand: " + dealerScript.handValue.ToString();
        hideCard.GetComponent<Renderer>().enabled = true;

        dealButton.gameObject.SetActive(false);
        betButton.gameObject.SetActive(false);
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
        standText.text = "Stand";

 
        betText.text = "Current Bet: \n" + "$" + pot.ToString();

        DoubleDownCheck();
      
    }

    private void HitClicked()
    {
        // Checks if there is still room on the board
        if(playerScript.cardIndex <= 10)
        {
            playerScript.GetCard();
            scoreText.text = "Your Hand: \n" + playerScript.handValue.ToString();
            if (playerScript.handValue > 20) RoundOver();
        }
    }

    private void StandClicked()
    {
        standClicks++;
        RoundOver();
        HitDealer();
    }

    private void DoubleClicked()
    {
        coinsController.DecrementCoins(pot);
        pot += pot;
        betText.text = "Current Bet: \n" + "$" + pot.ToString();

        if (playerScript.cardIndex <= 10)
        {
            playerScript.GetCard();
            scoreText.text = "Your Hand: \n" + playerScript.handValue.ToString();
        }
        standClicks++;
        RoundOver();
        HitDealer();
        doubleButton.gameObject.SetActive(false);
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
        pot += (intBet);
        betText.text = "Current Bet: \n" + "$" + pot.ToString();
    }

    void RoundOver()
    {
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;

        if (standClicks < 1 && !playerBust && !dealerBust && !player21 && !dealer21) return;

        bool roundOver = true;
        // Both sides bust, push happens
        if (playerBust && dealerBust)
        {
            mainText.text = "All Bust: Bets Returned";
            mainText.gameObject.SetActive(true);
            coinsController.IncrementCoins(pot);
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
            coinsController.IncrementCoins(pot * 2);
        }
        // Tie check, return bets
        else if (playerScript.handValue == dealerScript.handValue)
        {
            mainText.text = "Tie: Bets Returned";
            mainText.gameObject.SetActive(true);
            coinsController.IncrementCoins(pot);
        }
        else
        {
            roundOver = false;
        }

        if (roundOver)
        {
            hitButton.gameObject.SetActive(false);
            standButton.gameObject.SetActive(false);
            doubleButton.gameObject.SetActive(false);
            splitButton.gameObject.SetActive(false);
            dealButton.gameObject.SetActive(true);
            betButton.gameObject.SetActive(true);
            dealerScoreText.gameObject.SetActive(true);
            hideCard.GetComponent<Renderer>().enabled = false;
            standClicks = 0;
            pot = 0;
            betText.text = "Current Bet: \n" + "$" + pot.ToString();
        }
    }

    private void DoubleDownCheck()
    {
        if (coinsController.totalCoins >= pot)
        {
            switch (playerScript.handValue)
            {
                case 9:
                    doubleButton.gameObject.SetActive(true);
                    break;
                case 10:
                    doubleButton.gameObject.SetActive(true);
                    break;
                case 11:
                    doubleButton.gameObject.SetActive(true);
                    break;
            }
        }
        
    }

    private void SplitCheck()
    {
        int card1Value = GameObject.Find("PlayerCard1").GetComponent<CardScript>().value;
        int card2Value = GameObject.Find("PlayerCard2").GetComponent<CardScript>().value;
        if (card1Value == card2Value)
        {
            splitButton.gameObject.SetActive(true);
        }
    }
    public void PromptHide()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
}
