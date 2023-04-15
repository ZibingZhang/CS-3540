using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    //public static float mouseSensitivity = 5;
    public Slider sensitivitySlider;

    float pitch = 0;
    Transform playerBody;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = transform.parent.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (!LevelManager.levelPaused)
        {
            float moveX = Input.GetAxis("Mouse X") * MouseSensitivity.ms * Time.deltaTime;
            float moveY = Input.GetAxis("Mouse Y") * MouseSensitivity.ms * Time.deltaTime;

            // yaw
            playerBody.Rotate(Vector3.up * moveX);

            // pitch
            pitch -= moveY;

            pitch = Mathf.Clamp(pitch, -90f, 70f);
            transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
    }

    /*
    void SubmitSliderSetting()
    {
        mouseSensitivity = sensitivitySlider.value;
    }
    */

}
