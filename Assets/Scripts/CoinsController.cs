using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsController : MonoBehaviour
{
    public static CoinsController Instance { get; private set; }
    public int totalCoins;
    public int baseCoins;
    public GameObject coinsObj;
    public TextMeshProUGUI coinsText;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins", baseCoins);
        
        coinsObj = GameObject.Find("CoinsUI");
        coinsText = GameObject.Find("CoinsText").GetComponent<TextMeshProUGUI>();
        coinsText.text = "" + totalCoins;
    }

    void Update()
    {
        if (coinsObj == null)
        {
            coinsObj = GameObject.Find("CoinsUI");
        }

        if (coinsText == null)
        {
            coinsText = GameObject.Find("CoinsText").GetComponent<TextMeshProUGUI>();
            coinsText.text = "" + totalCoins;
        }
    }

    public void IncrementCoins(int numCoins)
    {
        Debug.Log("increment by " + numCoins);
        totalCoins += numCoins;
        coinsText.text = "" + totalCoins;
        SaveCoins();
    }

    public void DecrementCoins(int numCoins)
    {
        Debug.Log("decrement by " + numCoins);
        totalCoins -= numCoins;
        coinsText.text = "" + totalCoins;
        SaveCoins();
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.Save();
    }
}
