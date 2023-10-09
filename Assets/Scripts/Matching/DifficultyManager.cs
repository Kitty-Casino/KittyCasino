using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public CardManager cardManager;
    public GameObject startingScreen;

    void Start()
    {
        cardManager.enabled = false;
        SetDifficulty(0);
    }

    public void SetDifficulty(int level)
    {
        if (level == 0)
        {
            cardManager.level = 1;
        }

        if (level == 1)
        {
            cardManager.level = 2;
        }

        if (level == 2)
        {
            cardManager.level = 3;
        }
    }

    public void PlayButton()
    {
        cardManager.enabled = true;
        startingScreen.SetActive(false);
    }
}
