using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public GameObject[] cards;
    public GameObject[] cardDetails;
    public Sprite[] cardDecor;
    private int[] numValues;
    public int savedCard;
    public int finalCard;
    public int savedCardNum;
    public int finalCardNum;
    public bool hasBeenClicked;
    public bool coroutineOver = false;
    public int matchesMade;
    private bool isMatch;
    public int mistakesMade;
    public GameObject endScreen;
    private CoinsController coinController;
    public GameObject bettingUI;
    public bool betMade;
    public TMP_InputField bet;
    public int betValue;
    public TextMeshProUGUI winnings;

    void Start()
    {
        numValues = new int[GameObject.FindGameObjectsWithTag("Cards").Length]; // accounts for variable amt of cards
        coinController = GameObject.Find("CoinsController").GetComponent<CoinsController>();
        bettingUI.SetActive(false);
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
        bet.onEndEdit.AddListener(BetMade);
        endScreen.SetActive(false);

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

        int cardDecorNum = 0;
        int cardValue = 0;
        assignCards(cardDecorNum, cardValue);

        StartCoroutine(bettingScreen());
    }

    private void assignCards(int cardDecorNum, int cardValue)
    {
        // assigning the card faces to each card value
        for (int j = 0; j < cards.Length / 2; j++)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].GetComponent<CardController>().value == cardValue)
                {
                    cards[i].GetComponent<CardController>().cardDecor[1] = cardDecor[cardDecorNum];
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
            cardDetails[numCard].GetComponent<Image>().sprite = cards[numCard].GetComponent<CardController>().cardDecor[1];
        }

        savedCardNum = numCard;
    }

    public void flipSecondCard(int numCard)
    {
        // flips second picked card
        if (finalCard != -1)
        {
            cardDetails[numCard].GetComponent<Image>().sprite = cards[numCard].GetComponent<CardController>().cardDecor[1];
        }

        finalCardNum = numCard;

        if (cards[savedCardNum].GetComponent<CardController>().value == cards[finalCardNum].GetComponent<CardController>().value)
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
                cardDetails[i].GetComponent<Image>().sprite = cards[i].GetComponent<CardController>().cardDecor[1];
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
        coroutineOver = true;

        for (int i = 0; i < cards.Length; i++)
        {
            cardDetails[i].GetComponent<Image>().sprite = cards[i].GetComponent<CardController>().cardDecor[0];
        }
    }

    IEnumerator removeCards()
    {
        coroutineOver = false;
        yield return new WaitForSeconds(2);
        cards[savedCardNum].SetActive(false);
        cards[finalCardNum].SetActive(false);

        if (matchesMade != (cards.Length / 2))
        {
            resetValues();
        }
        else
        {
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
        cardDetails[savedCardNum].GetComponent<Image>().sprite = cards[savedCardNum].GetComponent<CardController>().cardDecor[0];
        cardDetails[finalCardNum].GetComponent<Image>().sprite = cards[finalCardNum].GetComponent<CardController>().cardDecor[0];
        resetValues();
        if (mistakesMade == 3)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cardDetails[i].GetComponent<Image>().sprite = cards[i].GetComponent<CardController>().cardDecor[0];
                cards[i].SetActive(false);
            }

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
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].SetActive(true);
            cardDetails[i].GetComponent<Image>().sprite = cards[i].GetComponent<CardController>().cardDecor[0];
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

    public void BetMade(string input)
    {
        betMade = true;
        Debug.Log(input);
        betValue = int.Parse(input);
        coinController.DecrementCoins(int.Parse(input));
    }

    private int calculateWinnings()
    {
        if (mistakesMade == 0)
        {
            return betValue * 2;
        }
        else if (mistakesMade == 1)
        {
            return betValue;
        }
        else if (mistakesMade == 2)
        {
            return betValue / 2;
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