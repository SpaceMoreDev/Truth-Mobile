using Behaviours;
using UnityEngine;
using UnityEngine.InputSystem;


delegate void Command();

class ActionControl
{
    private Command _command;

    public void SetCommand(Command command)
    {
        _command = command;
    }

    public void Start()
    {
        _command?.Invoke();
    }
}

public class PlayerActions : PlayerControl
{
    PlayerCommands commands;
    internal ActionControl actionControl;

    PlayerActions() {
        actionControl = new();
    }

    private void Start()
    {
        commands = GetComponent<PlayerCommands>();
        commands._playermovement = _playermovement;

        Movement.performed += E_Move;
        Attack.performed += E_Attack;
        Movement.canceled += E_Move;
        Look.performed += E_lookAround;
    }

    private void OnDestroy()
    {
        Movement.performed -= E_Move;
        Attack.performed -= E_Attack;
        Movement.canceled -= E_Move;
        Look.performed -= E_lookAround;
    }

    void E_Move(InputAction.CallbackContext ctx){
        commands.InputDirection = ctx.ReadValue<Vector2>();
    }

    void E_lookAround(InputAction.CallbackContext ctx){
        actionControl.SetCommand(commands.lookAround);
        actionControl.Start();
    }

    void E_Attack(InputAction.CallbackContext ctx) {
        actionControl.SetCommand(commands.StartAttack);
        actionControl.Start();  
    }

    void Jump(InputAction.CallbackContext ctx) {
        actionControl.SetCommand(commands.Jump);
        actionControl.Start();
    }
}
