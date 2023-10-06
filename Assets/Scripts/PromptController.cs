using UnityEngine;
using UnityEngine.SceneManagement;

public class PromptController : MonoBehaviour
{
    public GameObject promptPanel;

    private bool isPromptActive = false;

    public int sceneID;

    private void Start()
    {
        // Initially, hide the prompt canvas
        promptPanel.SetActive(false);
    }

    private void Update()
    {
        // These controls are compatible with mouse clicks, comment this part out when building the game for mobile 
        if (Input.GetMouseButtonDown(0))
        {
            // Perform a raycast to check if the click hits an object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If it hits an object, checks game objects for tags that correspond to their function
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject && hit.transform.tag == "Prompt")
                {
                    isPromptActive = true;
                }
            }
        }

        // These controls are compatible with touch, comment this part out when building the game for dekstop testing
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Perform a raycast to check if the tap hits this object
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // If it hits an object, checks game objects for tags that correspond to their function
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject && hit.transform.tag == "Prompt")
                    {
                        isPromptActive = true;
                    }
                }
            }
        }

        if (isPromptActive)
        {
            promptPanel.SetActive(true);
        }
    }

    
    // When this function is called, the sceneID integer will determine which scene is loaded, make sure to set the correct scene ID for the scene you are trying to transition to
    // If you need to add a new scene transition, add a new case and assign respective sceneID to the Game Object
    public void LoadAnotherScene()
    {
        switch (sceneID)
        {
            default:
                SceneManager.LoadScene("Casino");
                break;
            case 1:
                SceneManager.LoadScene("Matching");
                break;
            case 2:
                SceneManager.LoadScene("Blackjack");
                break;
            case 3:
                SceneManager.LoadScene("Slots");
                break;
            case 4:
                SceneManager.LoadScene("Shop");
                break;
        }
    }


    public void ClosePrompt()
    {
        // Hide the prompt canvas and reset the prompt text
        promptPanel.SetActive(false);

        isPromptActive = false;
    }

}
