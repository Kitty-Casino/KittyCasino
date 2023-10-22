using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void Start()
    {
        PlayerPrefs.DeleteAll();
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToCasino()
    {
        SceneManager.LoadScene(1);
    }
    

}
