using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorymodeController : MonoBehaviour
{
    public static StorymodeController Instance { get; private set; }
    
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
}
