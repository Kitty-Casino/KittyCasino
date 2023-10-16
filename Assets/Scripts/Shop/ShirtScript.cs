using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirtScript : MonoBehaviour
{
    public GameObject shirtPrefab;
    public void AttachShirtToPlayer()
    {
        if (PlayerCustomizationManager.instance != null)
        {
            PlayerCustomizationManager customizationManager = PlayerCustomizationManager.instance;

            if (shirtPrefab != null)
            {
                customizationManager.ApplyShirt(shirtPrefab);
            }
            else
            {
                GameObject emptyShirt = new GameObject();
                customizationManager.ApplyShirt(emptyShirt);
            }
        }
    }
}
