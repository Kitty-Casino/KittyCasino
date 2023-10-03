using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainManuUI;
    public GameObject settingsMenuUI;
    public SceneController sceneManager;

    void Start()
    {
        settingsMenuUI.SetActive(false);
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

    public void QuitButton()
    {
        Debug.Log("Quit The Game");
        Application.Quit();
    }
}
