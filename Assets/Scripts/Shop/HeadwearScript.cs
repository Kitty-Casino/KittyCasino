using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadwearScript : MonoBehaviour
{
    public GameObject hatPrefab;
    public void AttachHatToPlayer()
    {
        if (PlayerCustomizationManager.instance != null)
        {
            PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;

            if (hatPrefab != null)
            {
                customizationManager.ApplyHead(hatPrefab);
            }
            // else
            // {
            //     GameObject emptyHead = new GameObject();
            //     customizationManager.ApplyHead(emptyHead);
            // }
        }
    }
}
