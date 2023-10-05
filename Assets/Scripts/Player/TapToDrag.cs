using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToDrag : MonoBehaviour
{
    public float dragSpeed;

    public void Update()
    {
        // Checks for touch input and if true transforms the camera by the TouchDeltaPosition * dragSpeed
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 TouchDeltaPosition = Input.GetTouch(0).deltaPosition;

            transform.Translate(-TouchDeltaPosition.x * dragSpeed, -TouchDeltaPosition.y * dragSpeed, 0);

            // Sets the bounds for the camera, use position.y to determine camera height
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -40f, 40f),
                Mathf.Clamp(transform.position.y, 10f, 10f),
                Mathf.Clamp(transform.position.z, -40f, 40f));
        }
    }
}
