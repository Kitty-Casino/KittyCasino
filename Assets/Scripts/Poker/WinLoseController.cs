using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseController : MonoBehaviour
{
    public PokerManager pokerManager;

    public void ToCasino()
    {
        SceneManager.LoadScene(1);
    }

    public void Replay()
    {
        pokerManager.replayAmount++;
        pokerManager.Initialize();
    }
}
