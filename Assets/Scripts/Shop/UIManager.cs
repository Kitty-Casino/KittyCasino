using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject headwearView;
    public GameObject eyewearView;
    public GameObject neckView;
    public GameObject shirtView;
    public GameObject handwearView;
    public GameObject rightHandwearView;

    private GameObject currentPanel;

    //Sets the panels to hidden on start 
    private void Awake()
    {
        headwearView.SetActive(false);
        eyewearView.SetActive(false);
        neckView.SetActive(false);
        shirtView.SetActive(false);
        handwearView.SetActive(false);
        rightHandwearView.SetActive(false);
    }

    public void ShowPanel(GameObject panelToShow)
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false); // Deactivate the last opened panel.
        }

        panelToShow.SetActive(true); // Activate the new panel.
        currentPanel = panelToShow; // Update the reference to the current panel.
    }

    // These methods handle the panels through the method in ShopController, call these methods in their respective buttons to bring the panels up
    public void ShowHeadwearPanel()
    {
        ShowPanel(headwearView);
    }

    public void ShowEyewearPanel()
    {
        ShowPanel(eyewearView);
    }

    public void ShowNecklacePanel()
    {
        ShowPanel(neckView);
    }

    public void ShowShirtPanel()
    {
        ShowPanel(shirtView);
    }

    public void ShowHandwearPanel()
    {
        ShowPanel(handwearView);
    }

    public void ShowRightHandwearPanel()
    {
        ShowPanel(rightHandwearView);
    }
}
