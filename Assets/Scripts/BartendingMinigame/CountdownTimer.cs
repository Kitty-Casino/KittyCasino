using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public float currentTime = 15f;
    private bool active = true;

    void Update()
    {
        if (!active)
            return;

        currentTime -= Time.deltaTime;
        UpdateTimerUI();

        if (currentTime <= 0)
        {
            StopTimer();
        }
    }

    public void StopTimer()
    {
        active = false;
        UpdateTimerUI();
    }

    public void RestartTimer()
    {
        active = true;
        currentTime = 20f;
    }

    private void UpdateTimerUI()
    {
        TimeSpan timerTime = TimeSpan.FromSeconds(currentTime);
        countdownText.text = timerTime.ToString(@"\:s");
    }
}
