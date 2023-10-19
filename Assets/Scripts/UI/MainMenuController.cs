using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainManuUI;
    public GameObject settingsMenuUI;
    public SceneController sceneManager;
    public GameObject creditsUI;

    void Start()
    {
        settingsMenuUI.SetActive(false);
        creditsUI.SetActive(false);
    }

    public void PlayButton()
    {
        sceneManager.ToCasino();
    }

    public void SettingsButton()
    {
        mainManuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void CreditsButton()
    {
        mainManuUI.SetActive(false);
        creditsUI.SetActive(true);
    }

    public void CreditsBackButton()
    {
        mainManuUI.SetActive(true);
        creditsUI.SetActive(false);
    }

    public void QuitButton()
    {
        Debug.Log("Quit The Game");
        Application.Quit();
    }
}
