using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    bool touchMoved = false;
    NavMeshAgent agent;
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;
    [SerializeField] LayerMask blockRaycast;

    float lookRotationSpeed = 8f;
    float timer = 0f;
    [SerializeField] float mouseHoldTime;

    public Animator _animator;

    public Animator animator 
    { get
        {
            if (_animator == null)
            {
                _animator = GetComponentInChildren<Animator>();
            }
            return _animator;
        } 
        set { _animator = value; }
    }
    private bool isMoving;

    void Awake()
    {
        
        agent = GetComponent<NavMeshAgent>();
        
    }


    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            TapToMove();
        }

        ClickToMove();
        FaceTarget();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseApp();
        }

        isMoving = agent.velocity.magnitude > 0.1f;
        animator.SetBool("isMoving?", isMoving);
    }

    void TapToMove()
    {
        Touch touch = Input.GetTouch(0);
            
        if (touch.phase == TouchPhase.Moved)
        {
            touchMoved = true;
        }

        if (touch.phase == TouchPhase.Ended)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, blockRaycast))
            {
                Debug.Log("Block Raycast");
            }
            else if (Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hit, 1000, clickableLayers) && !touchMoved)
            {
                agent.destination = hit.point;
                if(clickEffect != null)
                {
                    int randRotation = Random.Range(0, 100);
                    Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                }
            }
            touchMoved = false;
            
        }
    }

    void ClickToMove()
    {
        if (Input.GetMouseButton(0)) 
        {
            timer += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0) && timer <= mouseHoldTime)
        {
            Debug.Log("Click!");
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, blockRaycast))
            {
                Debug.Log("Block Raycast");
            }
            else if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, clickableLayers))
            {
                agent.destination = hit.point;
                if (clickEffect != null)
                {
                    Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            timer = 0f;
            
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

    public void CloseApp()
    {
        Debug.Log("Quit The Game");
        Application.Quit();
    }
}
