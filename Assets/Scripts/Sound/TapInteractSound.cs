using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapInteractSound : MonoBehaviour
{
    public AudioClip tapSound;
    private DialogueTrigger dialogueTrigger;
    private AudioSource audioSource;
    private PlayerController player;
    bool touchMoved = false;

    void Start()
    {

        // Get the AudioSource component attached to this object
        audioSource = GetComponent<AudioSource>();

        dialogueTrigger = GetComponent<DialogueTrigger>();

        // Ensures that the AudioSource has an AudioClip assigned
        if (audioSource == null || tapSound == null)
        {
            Debug.LogError("Please assign an AudioSource and an AudioClip to this script.");
        }
    }

    void Update()
    {
        if (player == null && SceneManager.GetActiveScene().name == "Casino" || player == null && SceneManager.GetActiveScene().name == "StoryCasino")
        {
            StartCoroutine(WaitOneFrame());
        }

        // Check for touch input on mobile devices
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
                    if (hit.collider.gameObject == gameObject && hit.transform.tag == "Sound")
                    {
                        audioSource.PlayOneShot(tapSound);

                        if (this.gameObject.layer == LayerMask.NameToLayer("ManagerCat") && Vector3.Distance(gameObject.transform.position, player.transform.position) <= 5f)
                        {
                            dialogueTrigger.TriggerDialogue();
                        }
                    }
                }
                touchMoved = false;
            }
        }

        // Check for mouse click on PC
        if (Input.GetMouseButtonDown(0))
        {
            // Perform a raycast to check if the click hits an object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If it hits an object, checks game objects for tags that correspond to their function
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject && hit.transform.tag == "Sound")
                {
                    audioSource.PlayOneShot(tapSound);

                    if (this.gameObject.layer == LayerMask.NameToLayer("ManagerCat") && Vector3.Distance(gameObject.transform.position, player.transform.position) <= 5f)
                    {
                        dialogueTrigger.TriggerDialogue();
                    }
                }
            }
        }
    }

    IEnumerator WaitOneFrame()
    {
        yield return new WaitForSeconds(0.1f); // waits because of first frame on game load
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
}
