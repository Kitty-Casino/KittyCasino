using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NeckwearScript : MonoBehaviour
{
    public Material color;
    public Material baseDressColor;
    public Material baseRedShirtColor;
    public Material baseOverallColor;

    public int price;
    public TextMeshProUGUI priceText;
    public string customizationName;
    public Image equippedIcon;

    public delegate void Color(NeckwearScript neckwearScript);
    public static Color OnColorEquip;
    private void Start()
    {
        priceText.text = "" + price;
        UpdateVisualState();
    }
    private void OnEnable()
    {
        NeckwearScript.OnColorEquip += UpdateVisualState2;
    }

    private void OnDisable()
    {
        NeckwearScript.OnColorEquip -= UpdateVisualState2;
    }
    public void AttachNeckToPlayer()
    {
        bool isOwned = PlayerPrefs.GetInt(customizationName, 0) == 1;
        PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;

        string shirtName = customizationManager.currentShirt.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name;
        
        Debug.Log(shirtName);

        if (isOwned)
        {
            if (PlayerCustomizationManager.instance != null)
            {

                customizationManager.ClearColorEquipped();

                switch (shirtName)
                {
                    case "Cat_Base":
                        customizationManager.SetColorEquipped(color);
                        customizationManager.ApplyColor(color);
                        UpdateVisualState();
                        break;
                    case "Cat_Dress":
                        customizationManager.SetColorEquipped(baseDressColor);
                        customizationManager.ApplyColor(baseDressColor);
                        UpdateVisualState();
                        break;
                    case "Cat_Overall":
                        customizationManager.SetColorEquipped(baseOverallColor);
                        customizationManager.ApplyColor(baseOverallColor);
                        UpdateVisualState();
                        break;
                    case "Cat_RedShirt":
                        customizationManager.SetColorEquipped(baseRedShirtColor);
                        customizationManager.ApplyColor(baseRedShirtColor);
                        UpdateVisualState();
                        break;
                }
                /*
                if (color != null)
                {
                    customizationManager.SetColorEquipped(color);
                    customizationManager.ApplyColor(color);
                    UpdateVisualState();
                }
                */
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
                UpdateVisualState();

                if (PlayerCustomizationManager.instance != null)
                {
 
                    customizationManager.ClearColorEquipped();

                    switch (shirtName)
                    {
                        case "Cat_Base":
                            customizationManager.SetColorEquipped(color);
                            customizationManager.ApplyColor(color);
                            UpdateVisualState();
                            break;
                        case "Cat_Dress":
                            customizationManager.SetColorEquipped(baseDressColor);
                            customizationManager.ApplyColor(baseDressColor);
                            UpdateVisualState();
                            break;
                        case "Cat_Overall":
                            customizationManager.SetColorEquipped(baseOverallColor);
                            customizationManager.ApplyColor(baseOverallColor);
                            UpdateVisualState();
                            break;
                        case "Cat_RedShirt":
                            customizationManager.SetColorEquipped(baseRedShirtColor);
                            customizationManager.ApplyColor(baseRedShirtColor);
                            UpdateVisualState();
                            break;
                    }
                    /*
                    if (color != null)
                    {
                        customizationManager.SetColorEquipped(color);
                        customizationManager.ApplyColor(color);
                    }
                    */
                }
                
            }
            else
            {
                Debug.Log("Insufficient Coins");
            }
        }
    }

    private void UpdateVisualState()
    {
        PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;
        bool isOwned = PlayerPrefs.GetInt(customizationName, 0) == 1;
        bool isEquipped = customizationManager.IsColorEquipped(color);

        if (equippedIcon != null)
        {
            if (isOwned)
            {
                if (isEquipped)
                {
                    Debug.Log("Item Equipped");
                    equippedIcon.gameObject.SetActive(true);

                    NeckwearScript.OnColorEquip?.Invoke(this);
                }
                else
                {
                    Debug.Log("Item owned but not equipped");
                    equippedIcon.gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.Log("Item neither owned nor equipped");
                equippedIcon.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateVisualState2(NeckwearScript neckwearScript)
    {
        if (this == neckwearScript)
        {
            equippedIcon.gameObject.SetActive(true);
        }
        else
        {
            equippedIcon.gameObject.SetActive(false);
        }
    }
}
