using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class SlotsController : MonoBehaviour
{
    public int spinCost;
    CoinsController coinsController = CoinsController.Instance;

    public static event Action HandlePulled = delegate { };

    [SerializeField] private TextMeshProUGUI prizeText;
    [SerializeField] private RowScript[] rows;
    [SerializeField] private Transform handle;

    private bool resultsChecked = false;

    private bool pulled = false;

    // Defines payout values for 3 and 2 matches using dictionaries, if you want to modify payouts do that here
    private Dictionary<string, int> threeMatchesPayouts = new Dictionary<string, int>
    {
        { "Diamond", 200 },
        { "Crown", 400 },
        { "Melon", 600 },
        { "Bar", 800 },
        { "Seven", 1500 },
        { "Cherry", 3000 },
        { "Lemon", 5000 },
    };

    private Dictionary<string, int> twoMatchesPayouts = new Dictionary<string, int>
    {
        { "Diamond", 100 },
        { "Crown", 300 },
        { "Melon", 500 },
        { "Bar", 700 },
        { "Seven", 1000 },
        { "Cherry", 2000 },
        { "Lemon", 4000 },
    };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
        {
            prizeText.enabled = false;
            resultsChecked = false;
        }

        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked)
        {
            pulled = false;
            int prizeValue = CheckResults();
            prizeText.enabled = true;
            prizeText.text = "Prize: " + prizeValue;
        }
    }

    // Method that checks if the slot rows are stopped and if so, allows the PullHandle coroutine to begin and the slots to start spinning
    public void OnSlotPull()
    {
        if (coinsController.totalCoins >= spinCost && !pulled)
        {
            pulled = true;
            if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
            {
                coinsController.DecrementCoins(spinCost);
                StartCoroutine(PullHandle());
            }
        }
        else
        {
            Debug.Log("Insufficient coins");
        }
    }

    // Tells the slots to start spinning and the handle to rotate 
    private IEnumerator PullHandle()
    {
        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }

        HandlePulled();

        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, -i);
        }
    }

    // Determines the payouts of slots based on the results of the rows
    private int CheckResults()
    {
        string symbol1 = rows[0].stoppedSlot;
        string symbol2 = rows[1].stoppedSlot;
        string symbol3 = rows[2].stoppedSlot;

        // Check for three matches
        if (symbol1 == symbol2 && symbol2 == symbol3)
        {
            if (threeMatchesPayouts.ContainsKey(symbol1))
            {
                int prizeValue = threeMatchesPayouts[symbol1];
                coinsController.IncrementCoins(prizeValue);
                resultsChecked = true;
                return prizeValue;
            }
        }

        // Check for two matches
        if ((symbol1 == symbol2 && twoMatchesPayouts.ContainsKey(symbol1)) ||
            (symbol1 == symbol3 && twoMatchesPayouts.ContainsKey(symbol1)) ||
            (symbol2 == symbol3 && twoMatchesPayouts.ContainsKey(symbol2)))
        {
            string matchingSymbol = (symbol1 == symbol2) ? symbol1 : (symbol1 == symbol3) ? symbol1 : symbol2;
            int prizeValue = twoMatchesPayouts[matchingSymbol];
            coinsController.IncrementCoins(prizeValue);
            resultsChecked = true;
            return prizeValue;
        }

        resultsChecked = true;
        return 0; // No matches
    }
}
