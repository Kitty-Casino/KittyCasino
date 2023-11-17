using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WelcomeScreen : MonoBehaviour
{
    public GameObject howToPlayPanel;
    
    public void OpenHowToPanel()
    {
        howToPlayPanel.SetActive(true);
    }
    public void CloseHowToPanel()
    {
        howToPlayPanel.SetActive(false);
    }

    public void PlayButton()
    {
        gameObject.SetActive(false);
    }
}
