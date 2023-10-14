using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private GameObject currentPanel;

    public void ShowPanel(GameObject panelToShow)
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false); // Deactivate the last opened panel.
        }

        panelToShow.SetActive(true); // Activate the new panel.
        currentPanel = panelToShow; // Update the reference to the current panel.
    }
}
