using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapInteractSound : MonoBehaviour
{
    public AudioClip tapSound;
    private AudioSource audioSource;

    void Start()
    {

        // Get the AudioSource component attached to this object
        audioSource = GetComponent<AudioSource>();

        // Ensures that the AudioSource has an AudioClip assigned
        if (audioSource == null || tapSound == null)
        {
            Debug.LogError("Please assign an AudioSource and an AudioClip to this script.");
        }
    }

    void Update()
    {
        
        // Check for touch input on mobile devices
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Perform a raycast to check if the tap hits this object
               Ray ray = Camera.main.ScreenPointToRay(touch.position);
               RaycastHit hit;

            // If it hits an object, checks game objects for tags that correspond to their function
            if (Physics.Raycast(ray, out hit) )
            {
                if (hit.collider.gameObject == gameObject && hit.transform.tag == "Sound")
                {
                    audioSource.PlayOneShot(tapSound);
                }
            }
            }
        }
        


        // Check for mouse click on PC
        if (Input.GetMouseButtonDown(0))
        {
            // Perform a raycast to check if the click hits an object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If it hits an object, checks game objects for tags that correspond to their function
            if (Physics.Raycast(ray, out hit) )
            {
                if (hit.collider.gameObject == gameObject && hit.transform.tag == "Sound")
                {
                    audioSource.PlayOneShot(tapSound);
                }
            }
        }
       

    
    }

}
