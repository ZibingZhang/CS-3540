using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7;
    public float jumpHeight = 3;
    public float gravity = 9.81f;
    public float airControl = 2f;

    CharacterController controller;
    Vector3 input, moveDirection;

    private bool speedBoost;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
        if (speedBoost)
        {
            input *= moveSpeed*2;
        }
        else
        {
            input *= moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedBoost = !speedBoost;

        }

        if (controller.isGrounded)
        {
            moveDirection = input;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
            else
            {
                moveDirection.y = 0.0f;
            }
        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, Time.deltaTime * airControl);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(input * Time.deltaTime);
    }
}
