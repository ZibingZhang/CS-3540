using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public Slider slider;

    public void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        SetSensitivity(slider.value);
    }

    public void SetSensitivity(float value)
    {
        Debug.Log(value);
        MouseSensitivity.ms = value;
    }

}
