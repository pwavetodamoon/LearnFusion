
using System;
using Cysharp.Threading.Tasks;
using deVoid.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollectNetworkInputData : MonoBehaviour
{
    private Vector3 Direction;
    private bool isJumpButtonPress;
    private bool isPickUpPress;
    private bool isDropPress;
    private bool IsAttackPress;



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
        inputActions.Player.PickUp.performed += PickUp_performed;
        inputActions.Player.PickUp.canceled += PickUp_canceled;
        inputActions.Player.Drop.performed += Drop_performed;
        inputActions.Player.Drop.canceled += Drop_canceled;
        inputActions.Player.Attack.performed += Attack_performed;
        inputActions.Player.Attack.canceled += Attack_canceled;
        inputActions.PlayerKeyInputs.SendChat.performed += SendMessage_performed;   
        inputActions.PlayerKeyInputs.StartChat.performed += StartChat_performed;

    }

    private void StartChat_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("C");
        Signals.Get<OpenChatBox>().Dispatch();
    }

    private void SendMessage_performed(InputAction.CallbackContext obj)
    {
        Debug.Log("Enter");
        Signals.Get<SendPlayerMessage>().Dispatch();
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player.Move.performed -= Move_performed;
        inputActions.Player.Move.canceled -= Move_canceled;
        inputActions.Player.Jump.performed -= Jump_performed;
        inputActions.Player.Jump.canceled -= Jump_canceled;
        inputActions.Player.PickUp.performed -= PickUp_performed;
        inputActions.Player.PickUp.canceled -= PickUp_canceled;
        inputActions.Player.Drop.performed -= Drop_performed;
        inputActions.Player.Drop.canceled -= Drop_canceled;
        inputActions.Player.Attack.performed -= Attack_performed;
        inputActions.Player.Attack.canceled -= Attack_canceled;
        
        inputActions.PlayerKeyInputs.SendChat.performed -= SendMessage_performed;   
        inputActions.PlayerKeyInputs.StartChat.performed -= StartChat_performed;
        inputActions.PlayerKeyInputs.SendChat.canceled -= SendMessage_performed;   
        inputActions.PlayerKeyInputs.StartChat.canceled -= StartChat_performed;
    }


    private void Move_performed(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            var temp = inputActions.Player.Move.ReadValue<Vector2>();
            Direction = new Vector3(temp.x, 0f, temp.y);
        }

    }

    private void Move_canceled(InputAction.CallbackContext obj)
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
    private void PickUp_performed(InputAction.CallbackContext context)
    {
        isPickUpPress = true;
    }
    private void PickUp_canceled(InputAction.CallbackContext context)
    {
        isPickUpPress = false;
    }

    private void Drop_performed(InputAction.CallbackContext context)
    {
        isDropPress = true;
    }
    private void Drop_canceled(InputAction.CallbackContext context)
    {
        isDropPress = false;
    }

    private void Attack_performed(InputAction.CallbackContext context)
    {
        IsAttackPress = true;

    }
    private void Attack_canceled(InputAction.CallbackContext context)
    {
        IsAttackPress = false;
    }



    public NetworkInputData GetNetworkInputData()
    {
        //movement
        NetworkInputData networkInputData = new NetworkInputData();
        if (inputActions.Player.Move.IsPressed())
        {
            networkInputData.InputData = Direction.normalized;
        }
        // jump
        if (inputActions.Player.Jump.IsPressed())
        {
            networkInputData.JumpIsPressed = isJumpButtonPress;
        }
        // pick up
        if (inputActions.Player.PickUp.IsPressed())
        {
            networkInputData.IsPickUp = isPickUpPress;
        }
        // drop
        if (inputActions.Player.Drop.IsPressed())
        {
            networkInputData.IsDrop = isDropPress;
        }
        // attack
        if (inputActions.Player.Attack.IsPressed())
        {
            networkInputData.IsAttack = IsAttackPress;
        }
        return networkInputData;
    }
}