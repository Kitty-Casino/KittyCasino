using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShirtScript : MonoBehaviour
{
    public GameObject shirtPrefab;
    public int price;
    public TextMeshProUGUI priceText;
    public string customizationName;

    private void Start()
    {
        priceText.text = "" + price;
    }

    public void AttachShirtToPlayer()
    {
        bool isOwned = PlayerPrefs.GetInt(customizationName, 0) == 1;

        if (isOwned)
        {
            if (PlayerCustomizationManager.instance != null)
            {
                PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;

                if (shirtPrefab != null)
                {
                    customizationManager.ApplyShirt(shirtPrefab);
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

                    if (shirtPrefab != null)
                    {
                        customizationManager.ApplyShirt(shirtPrefab);
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
