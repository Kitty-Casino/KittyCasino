using UnityEngine;
using UnityEngine.SceneManagement;

public class PromptController : MonoBehaviour
{
    public GameObject promptCanvas;

    private bool isPromptActive = false;

    private void Start()
    {
        // Initially, hide the prompt canvas
        promptCanvas.SetActive(false);
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
            promptCanvas.SetActive(true);
        }
    }

    

    public void LoadAnotherScene()
    {
        SceneManager.LoadScene("Matching");
    }

    public void ClosePrompt()
    {
        // Hide the prompt canvas and reset the prompt text
        promptCanvas.SetActive(false);

        isPromptActive = false;
    }

}
