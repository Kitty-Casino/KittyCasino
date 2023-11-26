using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NeckwearScript : MonoBehaviour
{
    public Color color;

    public int price;
    public TextMeshProUGUI priceText;
    public string customizationName;
    public Image equippedIcon;

    public delegate void TheColor();
    public static TheColor OnColorEquip;
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

        if (isOwned)
        {
            if (PlayerCustomizationManager.instance != null)
            {

                customizationManager.ClearColorEquipped();

                if (color != null)
                {
                    customizationManager.SetColorEquipped(color);
                    customizationManager.ApplyColor(color);
                    OnColorEquip.Invoke();
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
 
                    customizationManager.ClearColorEquipped();

                    if (color != null)
                    {
                        customizationManager.SetColorEquipped(color);
                        customizationManager.ApplyColor(color);
                        OnColorEquip.Invoke();
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
