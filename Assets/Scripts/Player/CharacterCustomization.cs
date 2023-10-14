using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
   // These variables hold the position and prefab that is currently equipped to the player

    public Transform hatSlot;
    public GameObject currentHat;

    public Transform eyeSlot;
    public GameObject currentEyes;

    public Transform neckSlot;
    public GameObject currentNeck;

    public Transform shirtSlot;
    public GameObject currentShirt;

    public Transform handSlot;
    public GameObject currentHands;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // These methods remove the current piece of customization from the player

    public void RemoveHat()
    {
        if (currentHat != null)
        {
            Destroy(currentHat);
            currentHat = null;
        }
    }

    public void RemoveEyes()
    {
        if (currentEyes != null)
        {
            Destroy(currentEyes);

        }
    }

    public void RemoveNeck()
    {
        if (currentNeck != null)
        {
            Destroy(currentNeck);
        }
    }

    public void RemoveShirt()
    {
        if(currentShirt != null)
        {
            Destroy(currentShirt);
        }
    }

    public void RemoveHands()
    {
        if (currentHands != null)
        {
            Destroy(currentHands);
        }
    }


}
