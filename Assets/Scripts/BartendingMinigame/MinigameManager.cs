using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    void Awake()
    {
        System.Random rng = new System.Random();
        ingredient1 = rng.Next(0, maxIngredients);
        
        int tempIng2 = rng.Next(0, maxIngredients);
        ingredient2 = tempIng2 * 10;

        int tempIng3 = rng.Next(0, maxIngredients);
        ingredient3 = tempIng3 * 100;

        int tempIng4 = rng.Next(0, maxIngredients);
        ingredient4 = tempIng4 * 1000;

        int tempIng5 = rng.Next(0, maxIngredients);
        ingredient5 = tempIng5 * 10000;

        orderValue = ingredient1 + ingredient2 + ingredient3 + ingredient4 + ingredient5;
        Debug.Log("order value: " + orderValue);
    }
}
