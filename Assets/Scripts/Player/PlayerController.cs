using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    private Vector3 targetPosition;
    private bool isMoving;
    private bool isRotating;

    void Update()
    {
        // movement
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // calculates the target position based on touch position
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                targetPosition = hit.point;
                targetPosition.y = transform.position.y; // Keep the same height as the character
                isMoving = true;
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            isRotating = true;
        }

        if (Input.touchCount <= 0)
        {
            isRotating = false;
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            // moves the character towards the target position
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.Translate(moveDirection * speed * Time.deltaTime);

            // checks if the character has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }

        // if (isRotating && !isMoving)
        // {
        //     float rotationInput = Input.GetTouch(0).deltaPosition.x * rotationSpeed * Time.deltaTime;
        //     // Vector3 position = transform.position;
        //     // position.y = targetPosition.y; // lock the Y position
        //     // transform.position = position;
        //     transform.Rotate(Vector3.up, rotationInput);
        // }
    }
}
