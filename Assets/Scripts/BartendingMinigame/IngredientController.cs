using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    public MinigameManager minigameManager;
    public int value; // should be different for every ingredient

    void Start()
    {
        minigameManager = GameObject.Find("MinigameManager").GetComponent<MinigameManager>();
    }

    public void ingredientClicked()
    {
        // change drink colors?
        MinigameManager.drinkValue += value;
    }
}
