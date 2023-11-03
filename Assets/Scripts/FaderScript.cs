using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaderScript : MonoBehaviour
{
    // How fast object fades
    public float fadeSpeed = 10f;
    // How much object fades (lower = more fade)
    public float fadeAmount = 0.5f;
    // Controls whether object should be faded or not
    public bool doFade = false;
    // Stores original material opacity to restore the material to
    float originalOpacity;

    Material[] Mats;
    // Start is called before the first frame update
    void Start()
    {
        //Gets mesh renderer component and stashes the original opacity value of the alpha channel for the material
        Mats = GetComponent<Renderer>().materials;
        
        foreach(Material mat in Mats)
        {
            originalOpacity = mat.color.a;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doFade)
        
            FadeObject();
        



        else
        
            ResetFade();
        
            


    }
    void FadeObject()
    {
        //Set's the alpha to the fadeAmount float and the time it takes to fade is interpolated by Mathf.Lerp
        
        foreach (Material mat in Mats)
        {
            Color currentColor = mat.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));
            mat.color = smoothColor;
        }
    }

    void ResetFade()
    {
        //Restores the mesh material to it's original alpha channel value
        foreach (Material mat in Mats)
        {
            Color currentColor = mat.color;
            Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed * Time.deltaTime));
            mat.color = smoothColor;
        }
    }
}
