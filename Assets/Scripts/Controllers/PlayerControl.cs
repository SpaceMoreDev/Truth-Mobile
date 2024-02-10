using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Behaviours;
using UnityEditor.Timeline.Actions;

public class InputManager
{
    public static Core_Controls inputActions = new Core_Controls();
    public static event Action<InputActionMap> actionMapChange; // event if actionMap changed

    public static void ToggleActionMap(InputActionMap actionMap)
    {
        if(actionMap.enabled)
            return;

        inputActions.Disable();
        actionMapChange?.Invoke(actionMap);
        actionMap.Enable();
    }
}

public class PlayerControl : MonoBehaviour
{

    #region Public
    [HideInInspector]
    public CharacterController _controller;
    [HideInInspector]
    public Movement _playermovement;
    [SerializeField]
    public Transform camera;
    #endregion

    #region Private
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float rotationSpeed = 2f;
    [SerializeField]
    private float gravityValue = 18.81f;
    private float RotationSpeed
    {
        set { 
            if(_playermovement != null) _playermovement.TurnSpeed = rotationSpeed;
        }
        get{return rotationSpeed;}
    }
    #endregion

    #region Input Actions
    public  InputAction Movement;
    public  InputAction Look;
    public  InputAction Attack;
    #endregion

    void OnValidate() {
        RotationSpeed = rotationSpeed;
    }

    void Awake()
    {
        Movement = InputManager.inputActions.General.Move;
        Look = InputManager.inputActions.General.Look;
        Attack = InputManager.inputActions.General.Attack;

        InputManager.inputActions.General.Enable();
        _controller = GetComponent<CharacterController>();
        _playermovement = new Movement(_controller, camera, speed, rotationSpeed, gravityValue);

        InputSystem.onDeviceChange +=
        (device, change) =>
        {

            switch (change)
            {
                case InputDeviceChange.Added:
                    // New Device.
                    if (device is Gamepad)
                    {
                        print("Using Controller");
                    }
                    break;
                case InputDeviceChange.Disconnected:
                    // Device got unplugged.
                    break;
                case InputDeviceChange.Reconnected:
                    // Plugged back in.
                    break;
                case InputDeviceChange.Removed:
                    // Remove from Input System entirely; by default, Devices stay in the system once discovered.
                    break;
                default:
                    // See InputDeviceChange reference for other event types.
                    break;
            }
        };
    }

}
