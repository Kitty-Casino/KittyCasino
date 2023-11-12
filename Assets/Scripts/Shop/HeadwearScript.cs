using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeadwearScript : MonoBehaviour
{
    public GameObject hatPrefab;
    public int price;
    public TextMeshProUGUI priceText;
    public string customizationName;
    public Image ownedIcon;
    public Image equippedIcon;

    public delegate void Hat(GameObject currentObject);
    public static Hat OnHatEquip;

    private void Start()
    {
        priceText.text = "" + price;
        UpdateVisualState();
    }

    private void OnEnable()
    {
        HeadwearScript.OnHatEquip += UpdateVisualState2;
    }

    private void OnDisable()
    {
        HeadwearScript.OnHatEquip -= UpdateVisualState2;
    }

    public void AttachHatToPlayer()
    {
        bool isOwned = PlayerPrefs.GetInt(customizationName, 0) == 1;

        if (isOwned)
        {
            if (PlayerCustomizationManager.instance != null)
            {
                PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;

                customizationManager.ClearEquippedHead();

                if (hatPrefab != null)
                {
                    customizationManager.SetHeadEquipped(hatPrefab);
                    customizationManager.ApplyHead(hatPrefab);
                    UpdateVisualState();
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
                    customizationManager.ClearEquippedHead();
                    

                    if (hatPrefab != null)
                    {
                        customizationManager.SetHeadEquipped(hatPrefab);
                        customizationManager.ApplyHead(hatPrefab);
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
        bool isEquipped = customizationManager.IsHeadEquipped(hatPrefab);

        if (ownedIcon != null && equippedIcon != null)
        {
            if (isOwned)
            {
                if (isEquipped)
                {
                    Debug.Log("Item Equipped");
                    ownedIcon.gameObject.SetActive(false);
                    equippedIcon.gameObject.SetActive(true);

                    HeadwearScript.OnHatEquip?.Invoke(this.gameObject);
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
