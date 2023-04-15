using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{
<<<<<<< HEAD
    private static string mouseSensitivity = "mouseSensitivity";

    public static float ms
    {
        get
        {
            return PlayerPrefs.GetFloat(mouseSensitivity, 1);
        }
        set
        {
            PlayerPrefs.SetFloat(mouseSensitivity, value);
        }
=======

    public Slider sensitivitySlider;

    void SubmitSliderSetting()
    {
        MouseLook.mouseSensitivity = sensitivitySlider.value;
>>>>>>> 625a0d46065a51c0a74acb51d17bbf77f2f414aa
    }

}
