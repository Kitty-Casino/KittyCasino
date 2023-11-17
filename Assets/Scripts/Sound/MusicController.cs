using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip casinoMusic;
    public AudioClip gameMusic;
    public AudioClip shopMusic;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        switch (SceneManager.GetActiveScene().name)
        {
            case "Casino":
                PlaySound(casinoMusic);
                break;
            case "Shop":
                PlaySound(shopMusic);
                break;
            case "Bartending":
                PlaySound(gameMusic);
                break;
            case "Blackjack":
                PlaySound(gameMusic);
                break;
            case "Matching":
                PlaySound(gameMusic);
                break;
            case "Poker":
                PlaySound(gameMusic);
                break;
            case "Slots":
                PlaySound(gameMusic);
                break;
        }
            
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
