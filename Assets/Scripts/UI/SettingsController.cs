using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject settingsMenuUI;
    //public AudioMixer audioMixer;
    public bool canVibrate; // attach to things that can vibrate the phone

    void Start()
    {
        settingsMenuUI.SetActive(false);
        canVibrate = true;
    }

    public void BackButton()
    {
        menuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }

    public void SetVibration(bool condition)
    {
        canVibrate = condition;
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetVolume(float volume)
    {
        //audioMixer.SetFloat("volume", volume);
    }
}
