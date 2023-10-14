using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ShopController shopController;
    public GameObject headwearView;
    public GameObject eyewearView;
    public GameObject neckView;
    public GameObject shirtView;
    public GameObject handwearView;

    //Sets the panels to hidden on start 
    private void Awake()
    {
        headwearView.SetActive(false);
        eyewearView.SetActive(false);
        neckView.SetActive(false);
        shirtView.SetActive(false);
        handwearView.SetActive(false);
    }

    // These methods handle the panels through the method in ShopController, call these methods in their respective buttons to bring the panels up
    // Why did I make two scripts? I have no idea, I'll fix it later
    public void ShowHeadwearPanel()
    {
        shopController.ShowPanel(headwearView);
    }

    public void ShowEyewearPanel()
    {
        shopController.ShowPanel(eyewearView);
    }

    public void ShowNecklacePanel()
    {
        shopController.ShowPanel(neckView);
    }

    public void ShowShirtPanel()
    {
        shopController.ShowPanel(shirtView);
    }

    public void ShowHandwearPanel()
    {
        shopController.ShowPanel(handwearView);
    }
}
