using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{

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

    }

}
