using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MobileControl : MonoBehaviour
{
    

    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion


    #region InputActions

    InputAction TouchContact;
    InputAction TouchPosition;

    #endregion

    static Vector3 ScreenTowWorld(Camera camera, Vector3 position)
    {
        Ray ray = camera.ScreenPointToRay(position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            print($"clicked {hit.collider.name}");
        }
        return hit.point;
    }

    private void Start()
    {
        TouchContact = InputManager.inputActions.Touch.PrimaryContact;
        TouchPosition = InputManager.inputActions.Touch.PrimaryPosition;

        TouchContact.started += TouchStart;
        TouchContact.canceled += TouchEnd;
    }

    void TouchStart(InputAction.CallbackContext ctx)
    {
        if (OnStartTouch != null) { OnStartTouch(ScreenTowWorld(Camera.main, TouchPosition.ReadValue<Vector2>()), (float)ctx.time); }
    }

    void TouchEnd(InputAction.CallbackContext ctx)
    {
        if (OnEndTouch != null) { OnEndTouch(ScreenTowWorld(Camera.main, TouchPosition.ReadValue<Vector2>()), (float)ctx.time); }
    }

    public Vector2 PrimaryPosition()
    {
        return ScreenTowWorld(Camera.main, TouchPosition.ReadValue<Vector2>());
    }


}
