using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    PlayerCustomizationManager customizationManager;
    private void Start()
    {
      
        if (SceneManager.GetActiveScene().name == "Shop")
        {
            customizationManager = GameObject.Find("PlayerCustomizationManager").GetComponent<PlayerCustomizationManager>();
            customizationManager.InitializePlayer();
        }
        
        
      
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0); // fill with appropriate scene
    }

    public void ToCasino()
    {
        SceneManager.LoadScene(1);
    }
    

}
