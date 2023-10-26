using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientController : MonoBehaviour
{
    public int value;
    private MinigameManager minigameManager;

    void Start()
    {
        minigameManager = GameObject.Find("MinigameManager").GetComponent<MinigameManager>();
    }

    void Update()
    {
        // if (Input.touchCount > 0)
        // {
        //     Touch touch = Input.GetTouch(0);
        //     touchOver = false;

        //     if (touch.phase == TouchPhase.Ended)
        //     {
        //         RaycastHit hit;

        //         if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit, 1000, ingredientLayer) && !touchOver)
        //         {
        //             // change drink colors?
        //             MinigameManager.drinkValue += value;
        //             touchOver = true;
        //         }
        //     }
        // }
    }
}
