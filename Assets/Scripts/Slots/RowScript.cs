using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowScript : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;
    public bool rowStopped;
    public string stoppedSlot;
    // Start is called before the first frame update
    void Start()
    {
        rowStopped = true;
        SlotsController.HandlePulled += StartRotating;
    }

    public void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine("Rotate");
    }

    // Rotates through the slots based on the i < x in the for loop, adjust this for more/longer spins
    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.025f;

        for (int i = 0; i < 30; i++)
        {
            if (transform.position.y == -3.5)
            {
                transform.position = new Vector3(transform.position.x, 1.75f, 3f);
            }

            transform.position = new Vector3(transform.position.x, transform.position.y - 0.25f, 3f);
            yield return new WaitForSeconds(timeInterval);
        }

        // Randomly select one of the possible y-positions
        float[] possibleYPositions = { -3.5f, -2.75f, -2f, -1.25f, -0.5f, 0.25f, 1f, 1.75f };
        int randomIndex = Random.Range(0, possibleYPositions.Length);
        float selectedYPosition = possibleYPositions[randomIndex];

        transform.position = new Vector3(transform.position.x, selectedYPosition, 3f);

        // Set the corresponding slot based on the selected y-position
        if (selectedYPosition == -3.5f)
        {
            stoppedSlot = "Diamond";
        }
        else if (selectedYPosition == -2.75f)
        {
            stoppedSlot = "Crown";
        }
        else if (selectedYPosition == -2f)
        {
            stoppedSlot = "Melon";
        }
        else if (selectedYPosition == -1.25f)
        {
            stoppedSlot = "Bar";
        }
        else if (selectedYPosition == -0.5f)
        {
            stoppedSlot = "Seven";
        }
        else if (selectedYPosition == 0.25f)
        {
            stoppedSlot = "Cherry";
        }
        else if (selectedYPosition == 1f)
        {
            stoppedSlot = "Lemon";
        }
        else if (selectedYPosition == 1.75f)
        {
            stoppedSlot = "Diamond";
        }

        rowStopped = true;
    }

    // Unsubscribes to HandlePulled
    private void OnDestroy()
    {
        SlotsController.HandlePulled -= StartRotating;
    }

}
