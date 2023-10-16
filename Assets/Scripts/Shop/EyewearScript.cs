using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyewearScript : MonoBehaviour
{
    public GameObject eyesPrefab;
    public void AttachEyesToPlayer()
    {
        if (PlayerCustomizationManager.instance != null)
        {
            PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;

            if (eyesPrefab != null)
            {
                customizationManager.ApplyEyes(eyesPrefab);
            }
            else
            {
                GameObject emptyEyes = new GameObject();
                customizationManager.ApplyEyes(emptyEyes);
            }
        }
    }
}
