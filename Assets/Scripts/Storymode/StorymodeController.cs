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

    public void IncrementStoryProgression()
    {
        progression++;
        PlayerPrefs.SetInt("Progression", progression);
        PlayerPrefs.Save();
        Debug.Log("Story progress +1");
    }
}
