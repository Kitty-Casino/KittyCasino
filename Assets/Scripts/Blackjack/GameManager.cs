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
    public Button casinoButton;
    
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
        // Declares all buttons and UI elements and adds listeners for them
        

        mainText.gameObject.SetActive(false);

        dealButton.onClick.AddListener(() => DealClicked());
        hitButton.onClick.AddListener(() => HitClicked());
        standButton.onClick.AddListener(() => StandClicked());
        betButton.onClick.AddListener(() => BetClicked());
        doubleButton.onClick.AddListener(() => DoubleClicked());

        
    }
    
    // Handles dealing of the cards, resets both players and dealers hands and shuffles the deck and activates/deactivates approrpriate buttons
    private void DealClicked()
    {
        if (pot > 0)
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
            casinoButton.gameObject.SetActive(false);   
            hitButton.gameObject.SetActive(true);
            standButton.gameObject.SetActive(true);
            standText.text = "Stand";


            betText.text = "Current Bet: \n" + "$" + pot.ToString();

            DoubleDownCheck();
            if (playerScript.handValue > 20) RoundOver();
        }

    }

    // Gives player a new card and checks to see if they end up going over 20 and either hitting Blackjack or going bust
    private void HitClicked()
    {
        // Checks if there is still room on the board
        if(playerScript.cardIndex <= 10)
        {
            playerScript.GetCard();
            scoreText.text = "Your Hand: \n" + playerScript.handValue.ToString();
            if (playerScript.handValue > 20) RoundOver();
        }

        DoubleDownCheck();
    }

    // Immediately goes to game round resolution
    private void StandClicked()
    {
        standClicks++;
        HitDealer();
        RoundOver();
    }

    // If the player doubles down, takes another bet equal to initial bet, gives the player one card, and resolves the round (Sometimes it doesn't resolve though but you can still end via standing)
    private void DoubleClicked()
    {
        if (coinsController.totalCoins >= pot)
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
            HitDealer();
            RoundOver();
            doubleButton.gameObject.SetActive(false);
        }
    }

    // Whenever this is called, the dealer draws until the hand is greater than 17 and checks to see if they hit blackjack or bust
    private void HitDealer()
    {
        while (dealerScript.handValue < 17 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();
            dealerScoreText.text = "Dealer's Hand: " + dealerScript.handValue.ToString();
            if (dealerScript.handValue > 20) RoundOver();
        }
    }

    // Adds more bet to the pot, int bet can be adjusted to whatever seems reasonable (I can add a decrement bet button, and additional increments as well if needed)
    public void BetClicked()
    {
        
       int intBet = 20; 
       if (coinsController.totalCoins >= intBet)
       {
            coinsController.DecrementCoins(intBet);
            pot += (intBet);
            betText.text = "Current Bet: \n" + "$" + pot.ToString();
       }
    }

    // Resolves the game and handles all possible blackjack outcomes, then it sets itself back to false so it the next round of play can happen
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
            Debug.Log("Everyone bust!");
            mainText.text = "All Bust: Bets Returned";
            mainText.gameObject.SetActive(true);
            coinsController.IncrementCoins(pot);
        }
        // If player bust but dealer didn't, or dealer has more points, dealer wins
        else if (playerBust || dealer21 || (!dealerBust && dealerScript.handValue > playerScript.handValue))
        {
            Debug.Log("Dealer won");
            losePanel.SetActive(true);
        }
        // If dealer bust, player didn't, or player has more points, player wins
        else if (dealerBust || player21 || playerScript.handValue > dealerScript.handValue)
        {
            Debug.Log("Player won");
            winPanel.SetActive(true);
            coinsController.IncrementCoins(pot * 2);
        }
        // Tie check, return bets
        else if (playerScript.handValue == dealerScript.handValue || dealer21 && player21)
        {
            Debug.Log("Tie");
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
            casinoButton.gameObject.SetActive(true);
            hideCard.GetComponent<Renderer>().enabled = false;
            standClicks = 0;
            pot = 0;
            betText.text = "Current Bet: \n" + "$" + pot.ToString();
        }
    }

    // Check to see if player is eligeble to double down
    private void DoubleDownCheck()
    {
        if (coinsController.totalCoins >= pot)
        {
            switch (playerScript.handValue)
            {
                default:
                    doubleButton.gameObject.SetActive(false);
                    break;
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

    // Check to see if player is eligeble to split 
    private void SplitCheck()
    {
        int card1Value = GameObject.Find("PlayerCard1").GetComponent<CardScript>().value;
        int card2Value = GameObject.Find("PlayerCard2").GetComponent<CardScript>().value;
        if (card1Value == card2Value)
        {
            splitButton.gameObject.SetActive(true);
        }
    }

    // Hides win/lose panels
    public void PromptHide()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }
}
