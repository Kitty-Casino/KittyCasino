using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckwearScript : MonoBehaviour
{
    public GameObject neckPrefab;
    public void AttachNeckToPlayer()
    {
       if(PlayerCustomizationManager.instance != null)
        {
            PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;

            if(neckPrefab != null)
            {
                customizationManager.ApplyNeck(neckPrefab);
            }
            else
            {
                GameObject emptyNeck = new GameObject();
                customizationManager.ApplyNeck(emptyNeck);
            }
        }
    }
}
