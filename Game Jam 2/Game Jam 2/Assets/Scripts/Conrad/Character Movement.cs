using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private InputManager inputManager;

    // Unity Documentation: https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField]
    private bool faceTowardsMoveDirection = false;

    [SerializeField]
    private float playerSpeed = 2.0f;

    [SerializeField]
    private float jumpHeight = 1.0f;

    [SerializeField]
    private float gravityValue = -9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        // controller = gameObject.AddComponent<CharacterController>();
        //? makes the jumping more reactive.
        controller.minMoveDistance = 0;
        inputManager = InputManager.Instance;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 wasd = inputManager.GetPlayerWASDInput();
        Vector3 move = new Vector3(wasd.x, 0, wasd.y);
        // Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        // if (move != Vector3.zero)
        if (move != Vector3.zero && faceTowardsMoveDirection)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        // if (Input.GetButtonDown("Jump") && groundedPlayer)
        if (inputManager.GetPlayerJumpInputCurrentFrame() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
