using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private InputManager inputManager;

    // Unity Documentation: https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
    private CharacterController controller;

    private Animator animator;
    private bool isJumping;
    private bool isFalling;

    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField]
    private bool faceTowardsMoveDirection = false;

    [SerializeField]
    private float playerSpeed = 2.0f;

    [SerializeField]
    [Range(0.5f, 2.0f)]
    private float sprintModifier = 1.2f;
    private float movementModifier = 1.0f;

    [SerializeField]
    private float jumpHeight = 1.0f;

    [SerializeField]
    private float gravityValue = -9.81f;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        // controller = gameObject.AddComponent<CharacterController>();
        //? makes the jumping more reactive.
        controller.minMoveDistance = 0;
        inputManager = InputManager.Instance;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        animator.SetBool("isGrounded", controller.isGrounded);
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            isJumping = false;
            isFalling = false;
        }

        Vector2 wasd = inputManager.GetPlayerWASDInput();
        if (inputManager.GetPlayerSprintInput() > 0.0f && groundedPlayer)
        {
            movementModifier = sprintModifier;
            animator.SetBool("isSprinting", true);
        }
        else
        {
            movementModifier = 1.0f;
            animator.SetBool("isSprinting", false);
        }
        Vector3 move = new Vector3(wasd.x * movementModifier, 0, wasd.y * movementModifier);
        // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // controller.Move(move * Time.deltaTime * playerSpeed);
        animator.SetFloat("movement", Mathf.Abs(wasd.x) + Mathf.Abs(wasd.y));

        // if (move != Vector3.zero)
        if (move != Vector3.zero && faceTowardsMoveDirection)
        {
            transform.Find("DetectiveCat").transform.forward = move;
        }

        // Changes the height position of the player..
        // if (Input.GetButtonDown("Jump") && groundedPlayer)
        if (inputManager.GetPlayerJumpInputCurrentFrame() && groundedPlayer)
        {
            animator.SetBool("isJumping", true);
            isJumping = true;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // Decides if the player is jumping or falling
        if (!groundedPlayer)
        {
            if (isJumping && playerVelocity.y < 0 || playerVelocity.y < -2)
            {
                animator.SetBool("isFalling", true);
                isFalling = true;
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(((move * playerSpeed) + playerVelocity) * Time.deltaTime);
    }
}
