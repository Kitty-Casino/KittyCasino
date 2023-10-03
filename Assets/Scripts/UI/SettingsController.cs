using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject settingsMenuUI;

    void Start()
    {
        settingsMenuUI.SetActive(false);
    }

    public void BackButton()
    {
        menuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }
}
