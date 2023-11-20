using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static bool isInStoryMode = false;
    public void Start()
    {
        //if (SceneManager.GetActiveScene().name != "Casino" && SceneManager.GetActiveScene().name != "MainMenu")
        //{
        //    PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;
        //    customizationManager.UpdateLastVisitedScene();
        //}   
        // PlayerPrefs.DeleteAll();
        if (SceneManager.GetActiveScene().name == "StoryCasino")
        {
            isInStoryMode = true;
        }
    }
    public void ToMainMenu()
    {
        isInStoryMode = false;
        SceneManager.LoadScene(0);
    }

    public void ToCasino()
    {
        if (!isInStoryMode)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(8);
        }
        
    }
    
    public void ToStoryMode()
    {
        isInStoryMode = true;
        SceneManager.LoadScene(8);
    }
}
