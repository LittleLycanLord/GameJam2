using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //! ╔═══════════════════╗
    //! ║ SINGLETON CONTENT ║
    //! ╚═══════════════════╝
    private static InputManager instance;
    public static InputManager Instance
    {
        get { return instance; }
    }

    //! - - - - - - - - - - -
    private PlayerControls inputActions;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public Vector2 GetPlayerWASDInput()
    {
        return inputActions.GroundMovement.Walking.ReadValue<Vector2>();
    }

    public Vector2 GetPlayerSprintInput()
    {
        return inputActions.GroundMovement.Sprinting.ReadValue<Vector2>();
    }


    public bool GetPlayerJumpInputCurrentFrame()
    {
        return inputActions.GroundMovement.Jumping.triggered;
    }

    public bool GetPlayerInteractInputCurrentFrame()
    {
        return inputActions.GroundMovement.Interact.triggered;
    }
}
