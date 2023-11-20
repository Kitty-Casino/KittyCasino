using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShirtScript : MonoBehaviour
{
    public GameObject shirtPrefab;
    public int price;
    public TextMeshProUGUI priceText;
    public string customizationName;

    public Material color;
    public Material baseDressColor;
    public Material baseRedShirtColor;
    public Material baseOverallColor;

    public Image ownedIcon;
    public Image equippedIcon;

    public delegate void Shirt(GameObject currentObject);
    public static Shirt OnShirtEquip;
    private void Start()
    {
        priceText.text = "" + price;
        UpdateVisualState();
    }
    private void OnEnable()
    {
        ShirtScript.OnShirtEquip += UpdateVisualState2;
    }

    private void OnDisable()
    {
        ShirtScript.OnShirtEquip -= UpdateVisualState2;
    }

    public void AttachShirtToPlayer()
    {
        bool isOwned = PlayerPrefs.GetInt(customizationName, 0) == 1;

        string shirtName = shirtPrefab.GetComponentInChildren<SkinnedMeshRenderer>().name;
        Debug.Log(shirtName);
        if (isOwned)
        {
            if (PlayerCustomizationManager.instance != null)
            {
                PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;

                customizationManager.ClearShirtEquipped();

                if (shirtPrefab != null)
                {
                    customizationManager.SetShirtEquipped(shirtPrefab);
                    customizationManager.ApplyShirt(shirtPrefab);
                    UpdateVisualState();

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
                UpdateVisualState();

                if (PlayerCustomizationManager.instance != null)
                {
                    PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;
                    customizationManager.ClearShirtEquipped();

                    if (shirtPrefab != null)
                    {
                        customizationManager.SetShirtEquipped(shirtPrefab);
                        customizationManager.ApplyShirt(shirtPrefab);

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
                    }

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
        bool isEquipped = customizationManager.IsShirtEquipped(shirtPrefab);

        if (ownedIcon != null && equippedIcon != null)
        {
            if (isOwned)
            {
                if (isEquipped)
                {
                    Debug.Log("Item Equipped");
                    ownedIcon.gameObject.SetActive(false);
                    equippedIcon.gameObject.SetActive(true);

                    ShirtScript.OnShirtEquip?.Invoke(this.gameObject);
                }
                else
                {
                    Debug.Log("Item owned but not equipped");
                    ownedIcon.gameObject.SetActive(true);
                    equippedIcon.gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.Log("Item neither owned nor equipped");
                ownedIcon.gameObject.SetActive(false);
                equippedIcon.gameObject.SetActive(false);
            }
        }

    }

    private void UpdateVisualState2(GameObject currentObject)
    {
        if (this.gameObject == currentObject)
            return;
        else
        {
            ownedIcon.gameObject.SetActive(true);
            equippedIcon.gameObject.SetActive(false);
        }
    }

}
