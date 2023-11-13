using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class MinigameManager : MonoBehaviour
{
    [Header("Ingredients")]
    public int ingredient1;
    public int ingredient2;
    public int ingredient3;
    public int ingredient4;
    public int ingredient5;
    public int maxIngredients;
    public GameObject liquid;

    [Header("Computation")]
    public static int orderValue;
    public static int drinkValue;
    public LayerMask ingredientLayer;
    private bool touchOver = true;
    public int orderTimer;
    public ParticleSystem ps;

    [Header("UI")]
    public TextMeshProUGUI ingredientAmount1;
    public TextMeshProUGUI ingredientAmount2;
    public TextMeshProUGUI ingredientAmount3;
    public TextMeshProUGUI ingredientAmount4;
    public TextMeshProUGUI ingredientAmount5;
    public GameObject check;
    public GameObject x;

    [Header("References")]
    private CoinsController coinsController;
    private CountdownTimer countdownTimer;

    void Awake()
    {
        CreateOrder();
    }

    void Start()
    {
        coinsController = GameObject.Find("CoinsController").GetComponent<CoinsController>();
        countdownTimer = GameObject.Find("Timer").GetComponent<CountdownTimer>();
        liquid.SetActive(false);
        check.SetActive(false);
        x.SetActive(false);
    }

    private void CreateOrder()
    {
        System.Random rng = new System.Random();
        ingredient1 = rng.Next(0, maxIngredients);
        ingredientAmount1.text = "" + ingredient1;

        int tempIng2 = rng.Next(0, maxIngredients);
        ingredient2 = tempIng2 * 10;
        ingredientAmount2.text = "" + tempIng2;

        int tempIng3 = rng.Next(0, maxIngredients);
        ingredient3 = tempIng3 * 100;
        ingredientAmount3.text = "" + tempIng3;

        int tempIng4 = rng.Next(0, maxIngredients);
        ingredient4 = tempIng4 * 1000;
        ingredientAmount4.text = "" + tempIng4;

        int tempIng5 = rng.Next(0, maxIngredients);
        ingredient5 = tempIng5 * 10000;
        ingredientAmount5.text = "" + tempIng5;

        orderValue = ingredient1 + ingredient2 + ingredient3 + ingredient4 + ingredient5;
        Debug.Log("order value: " + orderValue);
    }

    void Update()
    {
        if (Input.touchCount > 0) // use touch
        {
            Touch touch = Input.GetTouch(0);
            touchOver = false;

            if (touch.phase == TouchPhase.Ended)
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit, 1000, ingredientLayer) && !touchOver)
                {
                    // change drink colors?
                    liquid.SetActive(true);
                    drinkValue += hit.transform.gameObject.GetComponent<IngredientController>().value;
                    Debug.Log("drink value: " + drinkValue);
                    touchOver = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0)) // use clicks
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, ingredientLayer))
            {
                hit.transform.gameObject.GetComponent<IngredientController>().selected = true;
                StartCoroutine(ChangeIngredientColor(hit.transform.gameObject));
                liquid.SetActive(true);
                drinkValue += hit.transform.gameObject.GetComponent<IngredientController>().value;
                Debug.Log("drink value: " + drinkValue);
            }
        }

        if (countdownTimer.currentTime <= 0f)
        {
            CheckDrink();
        }
    }

    IEnumerator ChangeIngredientColor(GameObject ingredient)
    {
        yield return new WaitForSeconds(0.5f);
        ingredient.GetComponent<IngredientController>().selected = false;
    }

    public void CheckDrink()
    {
        bool canEarnMoney = true;

        if (coinsController.totalCoins > 100)
        {
            canEarnMoney = false;
        }

        countdownTimer.StopTimer();

        if (orderValue == drinkValue)
        {
            StartCoroutine(WinLose(true));

            if (canEarnMoney)
            {
                coinsController.IncrementCoins((int)countdownTimer.currentTime);
            }
        }
        else
        {
            StartCoroutine(WinLose(false));
        }

        StartCoroutine(GetNewOrder());
    }

    IEnumerator WinLose(bool win)
    {
        ParticleSystem particles = ps;
        if (win)
        {
            check.SetActive(true);
            particles = Instantiate(ps, liquid.transform);
        }
        else
        {
            x.SetActive(true);
        }

        yield return new WaitForSeconds(0.75f);

        if (win)
        {
            check.SetActive(false);
            Destroy(particles);
        }
        else
        {
            x.SetActive(false);
        }
    }

    IEnumerator GetNewOrder()
    {
        yield return new WaitForSeconds(2);
        countdownTimer.RestartTimer();
        liquid.SetActive(false);
        CreateOrder();
        drinkValue = 0;
    }
}
