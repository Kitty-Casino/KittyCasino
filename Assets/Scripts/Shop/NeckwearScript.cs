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

    public delegate void Color();
    public static Color OnColorEquip;
    private void Start()
    {
        priceText.text = "" + price;
        UpdateVisualState();
    }
    private void OnEnable()
    {
        OnColorEquip += UpdateVisualState;
    }

    private void OnDisable()
    {
        OnColorEquip -= UpdateVisualState;
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
                        OnColorEquip.Invoke();
                        break;
                    case "Cat_Dress":
                        customizationManager.SetColorEquipped(baseDressColor);
                        customizationManager.ApplyColor(baseDressColor);
                        OnColorEquip.Invoke();
                        break;
                    case "Cat_Overall":
                        customizationManager.SetColorEquipped(baseOverallColor);
                        customizationManager.ApplyColor(baseOverallColor);
                        OnColorEquip.Invoke();
                        break;
                    case "Cat_RedShirt":
                        customizationManager.SetColorEquipped(baseRedShirtColor);
                        customizationManager.ApplyColor(baseRedShirtColor);
                        OnColorEquip.Invoke();
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

                if (PlayerCustomizationManager.instance != null)
                {
 
                    customizationManager.ClearColorEquipped();

                    switch (shirtName)
                    {
                        case "Cat_Base":
                            customizationManager.SetColorEquipped(color);
                            customizationManager.ApplyColor(color);
                            OnColorEquip.Invoke();
                            break;
                        case "Cat_Dress":
                            customizationManager.SetColorEquipped(baseDressColor);
                            customizationManager.ApplyColor(baseDressColor);
                            OnColorEquip.Invoke();
                            break;
                        case "Cat_Overall":
                            customizationManager.SetColorEquipped(baseOverallColor);
                            customizationManager.ApplyColor(baseOverallColor);
                            OnColorEquip.Invoke();
                            break;
                        case "Cat_RedShirt":
                            customizationManager.SetColorEquipped(baseRedShirtColor);
                            customizationManager.ApplyColor(baseRedShirtColor);
                            OnColorEquip.Invoke();
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
            if (isEquipped)
            {
                Debug.Log("Item equipped");
                equippedIcon.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("Item not equipped");
                equippedIcon.gameObject.SetActive(false);
            }
        }
    }
}
