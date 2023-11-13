using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EyewearScript : MonoBehaviour
{
    public GameObject eyesPrefab;
    public int price;
    public TextMeshProUGUI priceText;
    public string customizationName;

    public Image ownedIcon;
    public Image equippedIcon;

    public delegate void Eyewear(GameObject currentObject);
    public static Eyewear OnEyeEquip;
    private void Awake()
    {
        priceText.text = "" + price;
        UpdateVisualState();
    }
    private void OnEnable()
    {
        EyewearScript.OnEyeEquip += UpdateVisualState2;
    }

    private void OnDisable()
    {
        EyewearScript.OnEyeEquip -= UpdateVisualState2;
    }
    public void AttachEyesToPlayer()
    {
        bool isOwned = PlayerPrefs.GetInt(customizationName, 0) == 1;

        if (isOwned)
        {
            if (PlayerCustomizationManager.instance != null)
            {
                PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;

                customizationManager.ClearEyewearEquipped();

                if (eyesPrefab != null)
                {
                    customizationManager.SetEyewearEquipped(eyesPrefab);
                    customizationManager.ApplyEyes(eyesPrefab);
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
                    customizationManager.ClearEyewearEquipped();

                    if (eyesPrefab != null)
                    {
                        customizationManager.SetEyewearEquipped(eyesPrefab);
                        customizationManager.ApplyEyes(eyesPrefab);
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
        bool isEquipped = customizationManager.IsEyewearEquipped(eyesPrefab);

        if (ownedIcon != null && equippedIcon != null)
        {
            if (isOwned)
            {
                if (isEquipped)
                {
                    Debug.Log("Item Equipped");
                    ownedIcon.gameObject.SetActive(false);
                    equippedIcon.gameObject.SetActive(true);

                    EyewearScript.OnEyeEquip?.Invoke(this.gameObject);
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
