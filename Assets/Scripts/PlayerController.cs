using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7;
    [SerializeField] private float jumpHeight = 3;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float airControl = 2f;

    private CharacterController controller;
    private Vector3 input;
    private Vector3 moveDirection;
    private Vector3 startingPosition;

    private bool speedBoost;
    public bool resetLocation;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startingPosition = gameObject.transform.TransformPoint(Vector3.zero);
    }

    void Update()
    {
        if (!LevelManager.levelPaused)
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

        }
        
        // apply gravity to direction, move player
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(input * Time.deltaTime);
    
    }

    void LateUpdate()
    {
        if (resetLocation) {
            transform.position = startingPosition + new Vector3(0, 2, 0);
            Invoke("ResetLocationBool", 0.1f);
        }
    }

    private void ResetLocationBool() {
        resetLocation = false;
    }

    public void PlayAudioClip(AudioClip clip) {
        AudioSource.PlayClipAtPoint(clip, transform.position, 1);
        AudioSource.PlayClipAtPoint(clip, startingPosition, 1);
    }

    public void ChangeSpeed(float multiplier)
    {
        moveSpeed = multiplier * moveSpeed;
    }
}
