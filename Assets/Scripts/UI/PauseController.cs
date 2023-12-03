using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public SceneController sceneManager;
    public GameObject pauseButton;
    private GameObject christmasUI;

    public TMP_Text mainMenuButtonText;

    public SceneController sceneController;
    void Start()
    {
        Resume(); 
        if (SceneManager.GetActiveScene().name != "Casino" && SceneManager.GetActiveScene().name != "StoryCasino")
        {
            mainMenuButtonText.text = "To Casino";
        }
        else
        {
            mainMenuButtonText.text = "Main Menu";
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        PlayerController.EnablePlayerController?.Invoke(true);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        PlayerController.DisablePlayerController?.Invoke(false);

        christmasUI = GameObject.FindWithTag("Christmas");
        bool christmas = sceneController.ChristmasCheck();
        if (christmas)
        {
            christmasUI.SetActive(true);
        }
        else
        {
            christmasUI.SetActive(false);
        }
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;

        if (SceneManager.GetActiveScene().name != "Casino" && SceneManager.GetActiveScene().name != "StoryCasino")
        {
            sceneManager.ToCasino();
        }
        else
        {
            sceneManager.ToMainMenu();
        }
        
    }

    public void SettingsButton()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }
}
