using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandwearScript : MonoBehaviour
{
    public GameObject handsPrefab;
    public void AttachHandsToPlayer()
    {
        if (PlayerCustomizationManager.instance != null)
        {
            PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;

            if (handsPrefab != null)
            {
                customizationManager.ApplyHands(handsPrefab);
            }
            else
            {
                GameObject emptyHands = new GameObject();
                customizationManager.ApplyHands(emptyHands);
            }
        }
    }
}
