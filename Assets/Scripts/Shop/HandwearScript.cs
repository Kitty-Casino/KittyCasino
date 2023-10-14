using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandwearScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    public GameObject handsPrefab;
    CharacterCustomization charCustomization;

    void Awake()
    {
        charCustomization = player.GetComponent<CharacterCustomization>();
    }
    public void AttachHandsToPlayer()
    {
        if (handsPrefab != null && charCustomization.handSlot != null)
        {
            if (charCustomization.currentHands != null)
            {
                Destroy(charCustomization.currentHands);
            }

            charCustomization.currentHands = Instantiate(handsPrefab);

            charCustomization.currentHands.transform.SetParent(charCustomization.handSlot);

            charCustomization.currentHands.transform.localPosition = Vector3.zero;
            charCustomization.currentHands.transform.localRotation = Quaternion.identity;
        }
    }
}
