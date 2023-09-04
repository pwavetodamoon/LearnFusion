using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollectNetworkInputData : MonoBehaviour
{
    private Vector3 Direction;
    private bool isJumpButtonPress;
    private NewInputSystem inputActions;

    private void Awake()
    {
        inputActions = new NewInputSystem();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += Move_performed;
        inputActions.Player.Move.canceled += Move_canceled;
        inputActions.Player.Jump.performed += Jump_performed;
        inputActions.Player.Jump.canceled += Jump_canceled;


    }


    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player.Move.performed -= Move_performed;
        inputActions.Player.Move.canceled -= Move_canceled;
        inputActions.Player.Jump.performed -= Jump_performed;
        inputActions.Player.Jump.canceled -= Jump_canceled;

    }

    private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            var temp = inputActions.Player.Move.ReadValue<Vector2>();
            Direction = new Vector3(temp.x, 0f, temp.y);
        }

    }

    private void Move_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (obj.canceled)
        {
            Direction = Vector3.zero;
        }
    }
    private void Jump_performed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isJumpButtonPress = true;

        }
    }
    private void Jump_canceled(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            isJumpButtonPress = false;

        }
    }
    public NetworkInputData GetNetworkInputData()
    {
        //movem
        NetworkInputData networkInputData = new NetworkInputData();
        if (inputActions.Player.Move.IsPressed())
        {
            networkInputData.InputData = Direction.normalized;
        }

        // jump
        if (inputActions.Player.Jump.IsPressed())
            networkInputData.JumpIsPressed = isJumpButtonPress;

        return networkInputData;
    }
}