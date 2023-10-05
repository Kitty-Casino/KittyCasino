using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsController : MonoBehaviour
{
    public static CoinsController Instance { get; private set; }
    public int totalCoins;
    public int baseCoins;
    private GameObject coinsObj;
    private TextMeshProUGUI coinsText;

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
        totalCoins = baseCoins;
        
        coinsObj = GameObject.Find("CoinsUI");
        coinsText = GameObject.Find("CoinsText").GetComponent<TextMeshProUGUI>();
        coinsText.text = "" + totalCoins;
    }

    public void IncrementCoins(int numCoins)
    {
        Debug.Log("increment by " + numCoins);
        totalCoins += numCoins;
        coinsText.text = "" + totalCoins;
    }

    public void DecrementCoins(int numCoins)
    {
        Debug.Log("decrement by " + numCoins);
        totalCoins -= numCoins;
        coinsText.text = "" + totalCoins;
    }
}
