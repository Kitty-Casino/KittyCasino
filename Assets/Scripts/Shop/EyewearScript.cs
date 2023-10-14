using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyewearScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    public GameObject eyesPrefab;
    CharacterCustomization charCustomization;

    void Awake()
    {
        charCustomization = player.GetComponent<CharacterCustomization>();
    }
    public void AttachEyesToPlayer()
    {
        if (eyesPrefab != null && charCustomization.eyeSlot != null)
        {
            if (charCustomization.currentEyes != null)
            {
                Destroy(charCustomization.currentEyes);
            }

            charCustomization.currentEyes = Instantiate(eyesPrefab);

            charCustomization.currentEyes.transform.SetParent(charCustomization.eyeSlot);

            charCustomization.currentEyes.transform.localPosition = Vector3.zero;
            charCustomization.currentEyes.transform.localRotation = Quaternion.identity;
        }
    }
}
