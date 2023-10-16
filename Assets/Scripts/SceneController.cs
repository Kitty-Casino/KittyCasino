using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    PlayerCustomizationManager customizationManager;
    
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0); // fill with appropriate scene
    }

    public void ToCasino()
    {
        SceneManager.LoadScene(1);
    }
    

}
