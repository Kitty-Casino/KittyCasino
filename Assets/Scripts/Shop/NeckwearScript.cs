using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckwearScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    public GameObject neckPrefab;
    CharacterCustomization charCustomization;

    void Awake()
    {
        charCustomization = player.GetComponent<CharacterCustomization>();
    }
    public void AttachNeckToPlayer()
    {
        if (neckPrefab != null && charCustomization.neckSlot != null)
        {
            if (charCustomization.currentNeck != null)
            {
                Destroy(charCustomization.currentNeck);
            }

            charCustomization.currentNeck = Instantiate(neckPrefab);

            charCustomization.currentNeck.transform.SetParent(charCustomization.neckSlot);

            charCustomization.currentNeck.transform.localPosition = Vector3.zero;
            charCustomization.currentNeck.transform.localRotation = Quaternion.identity;
        }
    }
}
