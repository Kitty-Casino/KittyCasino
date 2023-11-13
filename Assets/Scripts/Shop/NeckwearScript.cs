using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NeckwearScript : MonoBehaviour
{
    public Material color;
    public int price;
    public TextMeshProUGUI priceText;
    public string customizationName;

    private void Awake()
    {
        priceText.text = "" + price;
    }

    public void AttachNeckToPlayer()
    {
        bool isOwned = PlayerPrefs.GetInt(customizationName, 0) == 1;

        if (isOwned)
        {
            if (PlayerCustomizationManager.instance != null)
            {
                PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;

                if (color != null)
                {
                    customizationManager.ApplyColor(color);
                }
            }

        }
        else
        {
            CoinsController coinsController = CoinsController.Instance;

            if (coinsController.totalCoins >= price)
            {
                coinsController.DecrementCoins(price);
                

                PlayerPrefs.SetInt(customizationName, 1);
                PlayerPrefs.Save();

                if (PlayerCustomizationManager.instance != null)
                {
                    PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;

                    if (color != null)
                    {
                        customizationManager.ApplyColor(color);
                    }

                }
                
            }
            else
            {
                Debug.Log("Insufficient Coins");
            }
        }
    }
}
