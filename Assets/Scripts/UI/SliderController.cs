using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderText;

    void Start()
    {
        slider.onValueChanged.AddListener((value) => {
            sliderText.text = value.ToString("0");
        });
    }
}
