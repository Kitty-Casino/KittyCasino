using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static bool isInStoryMode = false;
    public static bool isChristmas;
    private GameObject christmasDecorations;
    private Toggle christmasToggle;
    MusicController musicController;
    public void Start()
    {
        musicController = MusicController.instance;
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

        if (SceneManager.GetActiveScene().name == "Casino" || SceneManager.GetActiveScene().name == "StoryCasino"
            || SceneManager.GetActiveScene().name == "Bartending" || SceneManager.GetActiveScene().name == "Slots"
            || SceneManager.GetActiveScene().name == "Matching" || SceneManager.GetActiveScene().name == "Blackjack"
            || SceneManager.GetActiveScene().name == "Poker" || SceneManager.GetActiveScene().name == "MainMenu")
        {
            christmasDecorations = GameObject.FindWithTag("Christmas");
            christmasDecorations.SetActive(false);

            if (isChristmas)
            {
                christmasDecorations.SetActive(true);
            }
        }
        
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            christmasToggle = GameObject.Find("ChristmasToggle").GetComponent<Toggle>();

            if (isChristmas)
            {
                christmasToggle.isOn = true;
            }
            else
            {
                christmasToggle.isOn = false;
            }
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

    public void ToggleChristmas(bool christmasToggle)
    {
        if (christmasToggle)
        {
            Debug.Log("Christmas on");
            christmasToggle = true;
            musicController.PlayMusic(musicController.christmasMusic);
        }
        else
        {
            Debug.Log("Christmas off");
            christmasToggle = false;
            musicController.PlayMusic(musicController.casinoMusic);
        }
        isChristmas = christmasToggle;
        SetChristmasUI();
        
    }

    public void SetChristmasUI()
    {
        if (isChristmas)
        {
            christmasDecorations.SetActive(true);
        }
        else
        {
            christmasDecorations.SetActive(false);
        }
    }
    public bool ChristmasCheck()
    {
        if (isChristmas)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
