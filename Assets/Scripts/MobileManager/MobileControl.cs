using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MobileControl : MonoBehaviour
{
    InputActionMap InputMap;
    private void Start()
    {
        InputMap = InputManager.inputActions.General;
    }

    public void BTN_Attack()
    {

    }

}
