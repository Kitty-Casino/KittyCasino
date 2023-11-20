using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void Start()
    {
        //if (SceneManager.GetActiveScene().name != "Casino" && SceneManager.GetActiveScene().name != "MainMenu")
        //{
        //    PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;
        //    customizationManager.UpdateLastVisitedScene();
        //}   
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToCasino()
    {
        SceneManager.LoadScene(1);
    }
    
    public void ToStoryMode()
    {
        SceneManager.LoadScene(8);
    }
}
