using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    bool touchMoved = false;
    NavMeshAgent agent;
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;
    
    float lookRotationSpeed = 8f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            ClickToMove();
        }
        FaceTarget();
    }

    void ClickToMove()
    {
        Touch touch = Input.GetTouch(0);
            
        if (touch.phase == TouchPhase.Moved)
        {
            touchMoved = true;
        }

        if (touch.phase == TouchPhase.Ended)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit, 1000, clickableLayers) && !touchMoved)
            {
                agent.destination = hit.point;
                if(clickEffect != null)
                {
                    Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                }
            }
            touchMoved = false;
        }
    }

    void FaceTarget()
    {
        if (agent.velocity != Vector3.zero)
        {
            Vector3 direction = (agent.steeringTarget - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        }
    }

    //Peyton B's Code
    /*
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
    }*/
}
