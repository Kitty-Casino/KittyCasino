using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RightHandwearScript : MonoBehaviour
{
    public GameObject righthandsPrefab;
    public int price;
    public TextMeshProUGUI priceText;
    public string customizationName;

    public Image ownedIcon;
    public Image equippedIcon;

    public delegate void RightHand(GameObject currentObject);
    public static RightHand OnRightHandEquip;
    private void Start()
    {
        priceText.text = "" + price;
        UpdateVisualState();
    }
    private void OnEnable()
    {
        RightHandwearScript.OnRightHandEquip += UpdateVisualState2;
    }

    private void OnDisable()
    {
        RightHandwearScript.OnRightHandEquip -= UpdateVisualState2;
    }
    public void AttachRightHandToPlayer()
    {
        bool isOwned = PlayerPrefs.GetInt(customizationName, 0) == 1;

        if (isOwned)
        {
            if (PlayerCustomizationManager.instance != null)
            {
                PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;

                customizationManager.ClearRightHandEquipped();

                if (righthandsPrefab != null)
                {
                    customizationManager.SetRightHandEquipped(righthandsPrefab);
                    customizationManager.ApplyRightHands(righthandsPrefab);
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
                    customizationManager.ClearRightHandEquipped();

                    if (righthandsPrefab != null)
                    {
                        customizationManager.SetRightHandEquipped(righthandsPrefab);
                        customizationManager.ApplyRightHands(righthandsPrefab);
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
        bool isEquipped = customizationManager.IsRightHandEquipped(righthandsPrefab);

        if (ownedIcon != null && equippedIcon != null)
        {
            if (isOwned)
            {
                if (isEquipped)
                {
                    Debug.Log("Item Equipped");
                    ownedIcon.gameObject.SetActive(false);
                    equippedIcon.gameObject.SetActive(true);

                    RightHandwearScript.OnRightHandEquip?.Invoke(this.gameObject);
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
