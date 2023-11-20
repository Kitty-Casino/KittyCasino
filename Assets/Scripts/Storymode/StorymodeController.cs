using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorymodeController : MonoBehaviour
{
    public static StorymodeController Instance { get; private set; }
    public int progression;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        progression = 0;
        PlayerPrefs.SetInt("StoryProgression", 0);
        PlayerPrefs.Save();
    }

    public void IncrementStoryProgression()
    {
        progression++;
        PlayerPrefs.SetInt("StoryProgression", progression);
        PlayerPrefs.Save();
    }
}
