using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class MinigameManager : MonoBehaviour
{
    public static int orderValue;
    public static int drinkValue;
    public int ingredient1;
    public int ingredient2;
    public int ingredient3;
    public int ingredient4;
    public int ingredient5;
    public int maxIngredients;

    [Header("UI")]
    public TextMeshProUGUI ingredientAmount1;
    public TextMeshProUGUI ingredientAmount2;
    public TextMeshProUGUI ingredientAmount3;
    public TextMeshProUGUI ingredientAmount4;
    public TextMeshProUGUI ingredientAmount5;

    void Awake()
    {
        CreateOrder();
    }

    private void CreateOrder()
    {
        System.Random rng = new System.Random();
        ingredient1 = rng.Next(0, maxIngredients);
        ingredientAmount1.text = "" + ingredient1;
        
        int tempIng2 = rng.Next(0, maxIngredients);
        ingredient2 = tempIng2 * 10;
        ingredientAmount2.text = "" + (ingredient2 / 10);

        int tempIng3 = rng.Next(0, maxIngredients);
        ingredient3 = tempIng3 * 100;
        ingredientAmount3.text = "" + (ingredient3 / 100);

        int tempIng4 = rng.Next(0, maxIngredients);
        ingredient4 = tempIng4 * 1000;
        ingredientAmount4.text = "" + (ingredient4 / 1000);

        int tempIng5 = rng.Next(0, maxIngredients);
        ingredient5 = tempIng5 * 10000;
        ingredientAmount5.text = "" + (ingredient5 / 10000);

        orderValue = ingredient1 + ingredient2 + ingredient3 + ingredient4 + ingredient5;
        Debug.Log("order value: " + orderValue);
    }
}
