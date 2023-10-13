using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [Header("Cards")]
    public Sprite[] cardDecor;
    private int[] numValues;
    public int savedCard;
    public int finalCard;
    public int savedCardNum;
    public int finalCardNum;
    public bool hasBeenClicked;
    public bool coroutineOver = false;
    private CardController[] totalCards;

    [SerializeField]
    private GameObject[] cardsWithDifficulty;

    [SerializeField]
    private GameObject[] cards;

    [SerializeField]
    private GameObject[] totalDetails;

    [SerializeField]
    private GameObject[] cardDetails;

    [Header("Matches")]
    public int matchesMade;
    private bool isMatch;
    public int mistakesMade;
    public GameObject endScreen;

    [Header("Betting")]
    public int hardMultiplier;
    public Slider betSlider;
    public Button betButton;
    private CoinsController coinController;
    public GameObject bettingUI;
    public bool betMade;
    public int betValue;
    public TextMeshProUGUI winnings;
    private bool decremented = false;

    [Header("Difficulty")]
    public int level;
    public GameObject easy;
    public GameObject medium;
    public GameObject hard;
    
    void Start()
    {
        coinController = GameObject.Find("CoinsController").GetComponent<CoinsController>();
        bettingUI.SetActive(false);
        betButton.onClick.AddListener(BetMade);
        StartUp();
    }

    // initalizes all values to default
    void StartUp()
    {
        finalCard = -1;
        finalCardNum = -1;
        matchesMade = 0;
        isMatch = false;
        hasBeenClicked = false;
        mistakesMade = 0;
        betValue = 0;
        betMade = false;
        endScreen.SetActive(false);

        switch (level)
        {
            // EASY
            case 1:
                easy.SetActive(true);
                medium.SetActive(false);
                hard.SetActive(false);

                if (coinController.totalCoins >= 20)
                {
                    betSlider.maxValue = 20;
                }
                else
                {
                    betSlider.maxValue = coinController.totalCoins;
                }

                totalCards = new CardController[8];
                cardsWithDifficulty = new GameObject[8];
                totalDetails = new GameObject[8];
                totalCards = easy.GetComponentsInChildren<CardController>();

                int index1 = 0;
                foreach(CardController card in totalCards)
                {
                    cardsWithDifficulty[index1] = card.gameObject;
                    index1++;
                }

                break;

            // MEDIUM
            case 2:
                easy.SetActive(false);
                medium.SetActive(true);
                hard.SetActive(false);

                if (coinController.totalCoins >= 100)
                {
                    betSlider.maxValue = 100;
                }
                else
                {
                    betSlider.maxValue = coinController.totalCoins;
                }

                totalCards = new CardController[12];
                cardsWithDifficulty = new GameObject[12];
                totalDetails = new GameObject[12];
                totalCards = medium.GetComponentsInChildren<CardController>();

                int index2 = 0;
                foreach(CardController card in totalCards)
                {
                    cardsWithDifficulty[index2] = card.gameObject;
                    index2++;
                }

                break;

            // HARD
            case 3:
                easy.SetActive(false);
                medium.SetActive(false);
                hard.SetActive(true);

                if (coinController.totalCoins >= 200)
                {
                    betSlider.maxValue = 200;
                }
                else
                {
                    betSlider.maxValue = coinController.totalCoins;
                }

                totalCards = new CardController[16];
                cardsWithDifficulty = new GameObject[16];
                totalDetails = new GameObject[16];
                totalCards = hard.GetComponentsInChildren<CardController>();

                int index3 = 0;
                foreach(CardController card in totalCards)
                {
                    cardsWithDifficulty[index3] = card.gameObject;
                    index3++;
                }

                break;
        }

        numValues = new int[GameObject.FindGameObjectsWithTag("Cards").Length]; // accounts for variable amt of cards

        for (int i = 0; i < numValues.Length; i += 2)
        {
            numValues[i] = i;
            numValues[i + 1] = i;
        }

        shuffleCards(numValues);

        for (int i = 0; i < cardsWithDifficulty.Length; i++)
        {
            cardsWithDifficulty[i].GetComponent<CardController>().value = numValues[i]; 
        }

        int cardDecorNum = 0;
        int cardValue = 0;
        assignCards(cardDecorNum, cardValue);

        StartCoroutine(bettingScreen());
    }

    private void assignCards(int cardDecorNum, int cardValue)
    {    
        // assigning the card faces to each card value
        for (int j = 0; j < cardsWithDifficulty.Length / 2; j++)
        {
            for (int i = 0; i < cardsWithDifficulty.Length; i++)
            {
                if (cardsWithDifficulty[i].GetComponent<CardController>().value == cardValue)
                {
                    cardsWithDifficulty[i].GetComponent<CardController>().cardDecor[1] = cardDecor[cardDecorNum];
                }
            }

            cardValue += 2;
            cardDecorNum++;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseApp();
        }
    }

    public void flipFirstCard(int numCard)
    {
        // flip first picked card
        if (savedCard != -1)
        {
            cardsWithDifficulty[numCard].transform.GetChild(0).GetComponent<Image>().sprite = totalCards[numCard].GetComponent<CardController>().cardDecor[1];
        }

        savedCardNum = numCard;
    }

    public void flipSecondCard(int numCard)
    {
        // flips second picked card
        if (finalCard != -1)
        {
            cardsWithDifficulty[numCard].transform.GetChild(0).GetComponent<Image>().sprite = totalCards[numCard].GetComponent<CardController>().cardDecor[1];
        }

        finalCardNum = numCard;

        if (totalCards[savedCardNum].GetComponent<CardController>().value == totalCards[finalCardNum].GetComponent<CardController>().value)
        {
            isMatch = true;
        }

        if (savedCardNum != -1 && finalCardNum != -1 && isMatch)
        {
            StartCoroutine(removeCards());
        }
        else
        {
            StartCoroutine(hideCards());
        }
    }

    // flips 3 random cards with different values
    // <param> array of GameObjects of all the cards
    private void showRandomCards(GameObject[] cards)
    {
        int rand1 = Random.Range(0, cards.Length - 1);
        int temp = Random.Range(0, cards.Length - 1);
        int tempValue = cards[rand1].GetComponent<CardController>().value;
        int rand2 = 0;
        int rand3 = 0;
        bool flag = false;

        while (!flag)
        {
            temp = Random.Range(0, cards.Length - 1);
            if (temp != rand1 &&
            cards[temp].GetComponent<CardController>().value != cards[rand1].GetComponent<CardController>().value)
            {
                rand2 = temp;
                flag = true;
            }

        }
        flag = false;

        // determines card to be flipped last
        while (!flag)
        {
            temp = Random.Range(0, cards.Length - 1);

            if (temp != rand2 && temp != rand1 &&
            cards[temp].GetComponent<CardController>().value != cards[rand2].GetComponent<CardController>().value &&
            cards[temp].GetComponent<CardController>().value != cards[rand1].GetComponent<CardController>().value)
            {
                rand3 = temp;
                flag = true;
            }
        }

        for (int i = 0; i < cards.Length; i++)
        {
            if (i == rand1 || i == rand2 || i == rand3)
            {
                cardsWithDifficulty[i].transform.GetChild(0).GetComponent<Image>().sprite = cards[i].GetComponent<CardController>().cardDecor[1];
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
        showRandomCards(cardsWithDifficulty);  
        yield return new WaitForSeconds(5);
        coroutineOver = true;

        for (int i = 0; i < totalCards.Length; i++)
        {
            cardsWithDifficulty[i].transform.GetChild(0).GetComponent<Image>().sprite = cardsWithDifficulty[i].GetComponent<CardController>().cardDecor[0];
        }
    }

    IEnumerator removeCards()
    {
        coroutineOver = false;
        yield return new WaitForSeconds(2);
        cardsWithDifficulty[savedCardNum].SetActive(false);
        cardsWithDifficulty[finalCardNum].SetActive(false);

        if (matchesMade != (totalCards.Length / 2))
        {
            resetValues();
        }
        else
        {
            decremented = false;
            coinController.IncrementCoins(calculateWinnings());
            winnings.text = "You won " + calculateWinnings() + " coins!";
            endScreen.GetComponent<TMP_Text>().text = "YOU WIN!";
            endScreen.SetActive(true);
        }
    }

    IEnumerator hideCards()
    {
        coroutineOver = false;
        mistakesMade++;
        yield return new WaitForSeconds(2);
        cardsWithDifficulty[savedCardNum].transform.GetChild(0).GetComponent<Image>().sprite = totalCards[savedCardNum].GetComponent<CardController>().cardDecor[0];
        cardsWithDifficulty[finalCardNum].transform.GetChild(0).GetComponent<Image>().sprite = totalCards[finalCardNum].GetComponent<CardController>().cardDecor[0];
        resetValues();
        if (mistakesMade == 3)
        {
            for (int i = 0; i < totalCards.Length; i++)
            {
                cardsWithDifficulty[i].transform.GetChild(0).GetComponent<Image>().sprite = totalCards[i].GetComponent<CardController>().cardDecor[0];
                cardsWithDifficulty[i].SetActive(false);
            }

            decremented = false;
            coinController.IncrementCoins(calculateWinnings());
            winnings.text = "You won " + calculateWinnings() + " coins!";
            endScreen.GetComponent<TMP_Text>().text = "YOU LOSE!";
            endScreen.SetActive(true);
        }
    }

    // resets all values to default
    private void resetValues()
    {
        savedCard = -1;
        finalCard = -1;
        savedCardNum = -1;
        finalCardNum = -1;
        coroutineOver = true;
        hasBeenClicked = false;
        isMatch = false;
    }

    public void ReplayGame()
    {
        for (int i = 0; i < totalCards.Length; i++)
        {
            cardsWithDifficulty[i].SetActive(true); 
            cardsWithDifficulty[i].transform.GetChild(0).GetComponent<Image>().sprite = totalCards[i].GetComponent<CardController>().cardDecor[0];
        }
        StartUp();
    }

    // exposes betting UI and holds rest of game until bet is made
    // triggers card flip once bet has been made
    IEnumerator bettingScreen()
    {
        bettingUI.SetActive(true);

        while (!betMade)
        {
            yield return null;
        }

        bettingUI.SetActive(false);
        StartCoroutine(flipCards());
    }

    public void BetMade()
    {
        betMade = true;
        betValue = (int) betSlider.value;
        // betValue = int.Parse(input);

        if (!decremented)
        {
            decremented = true;
            coinController.DecrementCoins((int) betSlider.value);
        }
    }

    private int calculateWinnings()
    {
        if (mistakesMade == 0)
        {
            if (level == 3)
            {
                betValue *= hardMultiplier;
            }

            return betValue * 3;
        }
        else if (mistakesMade == 1)
        {
            if (level == 3)
            {
                betValue *= hardMultiplier;
            }

            return betValue * 2;
        }
        else if (mistakesMade == 2)
        {
            if (level == hardMultiplier)
            {
                betValue *= 2;
            }

            return betValue * 1;
        }
        else if (mistakesMade >= 3)
        {
            return 0;
        }

        return -1; // should never get here
    }

    public void CloseApp()
    {
        Debug.Log("Quit The Game");
        Application.Quit();
    }
}