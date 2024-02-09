using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerAnimController : MonoBehaviour
{
    #region Input Actions
    public static InputAction Movement;
    public static InputAction Attack;
    #endregion

    private Player _player;
    [SerializeField] private Animator _animator;

    [SerializeField] private VisualEffect VFX_Slash;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
        Movement = InputManager.inputActions.Movement.Move;
        Attack = InputManager.inputActions.Movement.Attack;
        Attack.performed += AttackBehavior;
    }

    // Update is called once per frame
    void AttackBehavior(InputAction.CallbackContext ctx)
    {
        AttackLogic();
    }
        

    public void AttackLogic()
    {
        _animator.Play("Attack");
        print("Attacked");
        VFX_Slash.Play();
    }

    private void FixedUpdate() {
        float speed =  Movement.ReadValue<Vector2>().magnitude;
        _animator.SetFloat("Speed",speed,0.15f,Time.fixedDeltaTime);
        print(speed);
    }
}
