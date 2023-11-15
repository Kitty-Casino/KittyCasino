using UnityEngine;
using UnityEngine.SceneManagement;

public class PromptController : MonoBehaviour
{
    public GameObject promptPanel;
    private bool isPromptActive = false;
    private bool isInRange = false;

    public float promptRange = 2;

    public string sceneID;
    bool touchMoved = false;

    private PlayerController playerController;
    [SerializeField] private RespawnController playerRespawn;

    private void Awake()
    {
        // Initially, hide the prompt canvas
        promptPanel.SetActive(false);
    }

    private void Update()
    {
        if (playerController == null)
        {
            playerController = GameObject.Find("Player(Clone)").GetComponent<PlayerController>();
        }

        if (playerRespawn == null) 
        {
            playerRespawn = GameObject.Find("Player(Clone)").GetComponent<RespawnController>();
        }

        // These controls are compatible with mouse clicks, comment this part out when building the game for mobile 
        if (Input.GetMouseButtonUp(0))
        {
            // Perform a raycast to check if the click hits an object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If it hits an object, checks game objects for tags that correspond to their function
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject && hit.transform.tag == "Prompt")
                {
                    if (Vector3.Distance(playerController.gameObject.transform.position, hit.transform.position) <= promptRange)
                    {
                        isPromptActive = true;
                    }
                    
                }
            }
        }

        // These controls are compatible with touch, comment this part out when building the game for dekstop testing
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Moved)
            {
                touchMoved = true;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                // Perform a raycast to check if the tap hits this object
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // If it hits an object, checks game objects for tags that correspond to their function
                if (Physics.Raycast(ray, out hit) && !touchMoved)
                {
                    if (hit.collider.gameObject == gameObject && hit.transform.tag == "Prompt")
                    {
                        if (Vector3.Distance(playerController.gameObject.transform.position, hit.transform.position) <= promptRange)
                        {
                            isPromptActive = true;
                        }
                    }
                }
                touchMoved = false;
            }
        }

        if (isPromptActive)
        {
            promptPanel.SetActive(true);
            playerController.enabled = false;
        }
    }

    
    // When this function is called, the sceneID integer will determine which scene is loaded, make sure to set the correct scene ID for the scene you are trying to transition to
    // If you need to add a new scene transition, add a new case and assign respective sceneID to the Game Object
    public void LoadAnotherScene()
    {
        playerRespawn.spawnPointX = playerController.transform.position.x;
        playerRespawn.spawnPointY = playerController.transform.position.y;
        playerRespawn.spawnPointZ = playerController.transform.position.z;
        PlayerPrefs.SetFloat("SpawnPointX", playerRespawn.spawnPointX);
        PlayerPrefs.SetFloat("SpawnPointY", playerRespawn.spawnPointY);
        PlayerPrefs.SetFloat("SpawnPointZ", playerRespawn.spawnPointZ);

        switch (sceneID)
        {
            default:
                SceneManager.LoadScene("Casino");
                break;
            case "Matching":
                SceneManager.LoadScene("Matching");
                break;
            case "Shop":
                SceneManager.LoadScene("Shop");
                break;
            case "Poker":
                SceneManager.LoadScene("Poker");
                break;
            case "Blackjack":
                SceneManager.LoadScene("Blackjack");
                break;
            case "Bartending":
                SceneManager.LoadScene("Bartending");
                break;
            case "Slots":
                SceneManager.LoadScene("Slots");
                break;
        }
    }

    public void ClosePrompt()
    {
        // Hide the prompt canvas and reset the prompt text
        Debug.Log("Close Prompt");
        promptPanel.SetActive(false);

        isPromptActive = false;
        playerController.enabled = true;
    }

    // Handles the enabling of the isInRange boolean to determine whether the prompt should be opened or not 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
    }

}
