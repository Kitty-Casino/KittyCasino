using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadwearScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    public GameObject hatPrefab;
    CharacterCustomization charCustomization;

    void Awake()
    {
        charCustomization = player.GetComponent<CharacterCustomization>();
    }
    public void AttachHatToPlayer()
    {
        if (hatPrefab != null && charCustomization.hatSlot != null)
        {
            if (charCustomization.currentHat != null)
            {
                Destroy(charCustomization.currentHat);
            }

            charCustomization.currentHat = Instantiate(hatPrefab);

            charCustomization.currentHat.transform.SetParent(charCustomization.hatSlot);

            charCustomization.currentHat.transform.localPosition = Vector3.zero;
            charCustomization.currentHat.transform.localRotation = Quaternion.identity;
        }
    }
}
