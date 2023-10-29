using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DailyRewardsSystem : MonoBehaviour
{
    [Header("UI")]
    public GameObject rewardsUI;
    public Button claimRewardButton;
    private CoinsController coinsController;

    [Header("Rewards Data")]
    [SerializeField] private double rewardDelay = 24f;
    [SerializeField] private float checkRewardDelay = 1f; // checks for new reward every 1 hr
    private bool isRewardReady;

    void Start()
    {
        isRewardReady = false;
        coinsController = GameObject.Find("CoinsController").GetComponent<CoinsController>();
        claimRewardButton.onClick.RemoveAllListeners();

        // check if game is opened for the first time
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("RewardClaim_DateTime")))
        {
            PlayerPrefs.SetString("RewardClaim_DateTime", DateTime.Now.ToString());
        }

        StopAllCoroutines();
        StartCoroutine(CheckForReward());
        CheckForReward();
    }

    IEnumerator CheckForReward()
    {
        while (true)
        {
            if (!isRewardReady)
            {
                DateTime currentDateTime = DateTime.Now;
                DateTime rewardClaimTime = DateTime.Parse(PlayerPrefs.GetString("RewardClaim_DateTime", currentDateTime.ToString()));

                // get total seconds between these times - SWITCH TO HOURS AFTER TESTING
                double elapsedSeconds = (currentDateTime - rewardClaimTime).TotalHours;

                if (elapsedSeconds >= rewardDelay)
                {
                    ActivateReward();
                }
                else
                {
                    DeactivateReward();
                }
            }

            yield return new WaitForSeconds(checkRewardDelay);
        }
    }

    private void ActivateReward()
    {
        isRewardReady = true;
        rewardsUI.SetActive(true);
    }

    private void DeactivateReward()
    {
        isRewardReady = false;
        rewardsUI.SetActive(false);
    }

    public void ClaimReward()
    {
        // save time of last reward claimed
        PlayerPrefs.SetString("RewardClaim_DateTime", DateTime.Now.ToString());

        coinsController.IncrementCoins(100);
        DeactivateReward();
    }
}


