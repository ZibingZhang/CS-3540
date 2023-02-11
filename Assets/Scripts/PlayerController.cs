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

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Player movement controls: WASD and arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // vectorizing directional control
        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;

        // speedboost toggling
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedBoost = !speedBoost;

        }
        // adding speed to direction with speedboost
        if (speedBoost)
        {
            input *= moveSpeed*2;
        }
        else
        {
            input *= moveSpeed;
        }

        // validating jumps
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
            // controlling mid-air movement
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, Time.deltaTime * airControl);
        }

        // apply gravity to direction, move player
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(input * Time.deltaTime);
    }
}
