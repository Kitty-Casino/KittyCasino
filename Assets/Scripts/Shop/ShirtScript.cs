using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirtScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    public GameObject shirtPrefab;
    CharacterCustomization charCustomization;

    void Awake()
    {
        charCustomization = player.GetComponent<CharacterCustomization>();
    }
    public void AttachShirtToPlayer()
    {
        if (shirtPrefab != null && charCustomization.shirtSlot != null)
        {
            if (charCustomization.currentShirt != null)
            {
                Destroy(charCustomization.currentShirt);
            }

            charCustomization.currentShirt = Instantiate(shirtPrefab);

            charCustomization.currentShirt.transform.SetParent(charCustomization.shirtSlot);

            charCustomization.currentShirt.transform.localPosition = Vector3.zero;
            charCustomization.currentShirt.transform.localRotation = Quaternion.identity;
        }
    }
}
