using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{

    public Slider sensitivitySlider;

    void SubmitSliderSetting()
    {
        MouseLook.mouseSensitivity = sensitivitySlider.value;
    }

}
