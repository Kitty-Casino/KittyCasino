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

    public Image ownedIcon;
    public Image equippedIcon;

    public delegate void Shirt();
    public static Shirt OnShirtEquip;
    private void Start()
    {
        priceText.text = "" + price;
        UpdateVisualState();
    }
    private void OnEnable()
    {
        OnShirtEquip += UpdateVisualState;
    }

    private void OnDisable()
    {
        OnShirtEquip -= UpdateVisualState;
    }

    public void AttachShirtToPlayer()
    {
        bool isOwned = PlayerPrefs.GetInt(customizationName, 0) == 1;

        if (isOwned)
        {
            if (PlayerCustomizationManager.instance != null)
            {
                PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;

                
                customizationManager.ClearShirtEquipped();
                customizationManager.ClearColorEquipped();

                if (shirtPrefab != null)
                {
                    customizationManager.SetShirtEquipped(shirtPrefab);
                    customizationManager.ApplyShirt(shirtPrefab);
                    OnShirtEquip?.Invoke();
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
                    customizationManager.ClearShirtEquipped();

                    if (shirtPrefab != null)
                    {
                        customizationManager.SetShirtEquipped(shirtPrefab);
                        customizationManager.ApplyShirt(shirtPrefab);
                        OnShirtEquip?.Invoke();
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
            if (isEquipped)
            {
                Debug.Log("Item Equipped");
                ownedIcon.gameObject.SetActive(false);
                equippedIcon.gameObject.SetActive(true);
            }
            else if (isOwned)
            {
                Debug.Log("Item owned but not equipped");
                ownedIcon.gameObject.SetActive(true);
                equippedIcon.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Item neither owned nor equipped");
                ownedIcon.gameObject.SetActive(false);
                equippedIcon.gameObject.SetActive(false);
            }
        }
    }
}
