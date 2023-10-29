using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DailyRewardsSystem : MonoBehaviour
{
    [Header("UI")]
    public GameObject rewardsUI;
    public Button claimRewardButton;
    private CoinsController coinsController;

    [Header("Rewards Data")]
    [SerializeField] private double rewardDelay = 86400f; // whole thing breaks unless i use seconds. not hours. idk man
    [SerializeField] private float checkRewardDelay = 3600f; // checks for new reward every 1 hr (in seconds)
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

                // get total seconds between these times
                double elapsedSeconds = (currentDateTime - rewardClaimTime).TotalSeconds;
                Debug.Log("elapsed seconds: " + elapsedSeconds);

                if (elapsedSeconds >= rewardDelay)
                {
                    if (SceneManager.GetActiveScene().buildIndex != 1) // if not in casino scene when 24 hours is up
                    {
                        StartCoroutine(WaitForCasino());
                    }
                    else
                    {
                        ActivateReward();
                    }
                }
                else
                {
                    DeactivateReward();
                }
            }

            yield return new WaitForSeconds(checkRewardDelay);
        }
    }

    IEnumerator WaitForCasino()
    {
        Debug.Log("here");
        while (SceneManager.GetActiveScene().buildIndex != 1) // waits until in Casino to activate Daily Reward
        {
            yield return null;
        }

        Debug.Log("here 1");
        ActivateReward();
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


