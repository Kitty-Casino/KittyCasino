using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToDrag : MonoBehaviour
{
    public float dragSpeed;
    public float mouseDragSpeed;

    public void Update()
    {
        // These controls are compatible with touch, comment this part out when building the game for desktop testing
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // Calculate the new position by translating based on touch input
            Vector3 newPosition = transform.position - new Vector3(touchDeltaPosition.x * dragSpeed, 0, touchDeltaPosition.y * dragSpeed);

            // Clamp the new position to the desired bounds
            newPosition.x = Mathf.Clamp(newPosition.x, -40f, 40f);
            newPosition.z = Mathf.Clamp(newPosition.z, -40f, 40f);

            // You might want to clamp the height (y position) as well to set camera height
            newPosition.y = Mathf.Clamp(newPosition.y, 10f, 10f);

            // Apply the new position
            transform.position = newPosition;
        }
        

        // These controls are compatible with mouse clicks, comment this part out when building the game for mobile
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Calculate the new position by translating based on mouse input
            Vector3 newPosition = transform.position - new Vector3(mouseX * mouseDragSpeed, 0, mouseY * mouseDragSpeed);

            // Clamp the new position to the desired bounds
            newPosition.x = Mathf.Clamp(newPosition.x, -40f, 40f);
            newPosition.z = Mathf.Clamp(newPosition.z, -40f, 40f);

            // You might want to clamp the height (y position) as well to set camera height
            newPosition.y = Mathf.Clamp(newPosition.y, 10f, 10f);

            // Apply the new position
            transform.position = newPosition;
        }

    }
}
