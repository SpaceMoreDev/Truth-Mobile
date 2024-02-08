using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Behaviours;
using UnityEngine.VFX;


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

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    private Movement _playermovement;


    #region Input Actions
    public static InputAction Movement;
    public static InputAction Look;
    #endregion


    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float gravityValue;

    [SerializeField]
    private Transform camera;

    [SerializeField]
    private VisualEffect VFX_Slash;

    void Awake()
    {
        Movement = InputManager.inputActions.Movement.Move;
        Look = InputManager.inputActions.Movement.Look; 
        InputManager.ToggleActionMap(InputManager.inputActions.Movement);
    }

    public void Attack()
    {
        print("Attacked");
        VFX_Slash.Play();
    }

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _playermovement = new Movement(_controller, speed, gravityValue, camera, speed/2);
        Look.performed += lookAround;

        InputSystem.onDeviceChange +=
        (device, change) =>
        {
           
            switch (change)
            {
                case InputDeviceChange.Added:
                    // New Device.
                    if(device is Gamepad)
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

    void lookAround(InputAction.CallbackContext ctx)
    {
    }

    private void FixedUpdate() {
        _playermovement.Move(Time.fixedDeltaTime, Movement.ReadValue<Vector2>());
    }
}
