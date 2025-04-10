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

    public delegate void Hat();
    public static event Hat OnHatEquip;

    void Start()
    {
        priceText.text = "" + price;
        UpdateVisualState();
    }

    private void OnEnable()
    {
        OnHatEquip += UpdateVisualState;
    }

    private void OnDisable()
    {
        OnHatEquip -= UpdateVisualState;
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
                    OnHatEquip?.Invoke();
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
                    customizationManager.ClearEquippedHead();

                    if (hatPrefab != null)
                    {
                        customizationManager.SetHeadEquipped(hatPrefab);
                        customizationManager.ApplyHead(hatPrefab);
                        OnHatEquip?.Invoke();
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
