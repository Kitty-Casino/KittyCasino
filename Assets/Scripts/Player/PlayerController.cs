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
}
