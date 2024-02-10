using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using Behaviours;
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
        VFX_Slash.Play();

        Vector3 movementVelocity = transform.forward * 6f;
        _player._playermovement.SetVelocity(new Vector3(movementVelocity.x,0,movementVelocity.z));
        Invoke("EndAttack",0.2f);
    }
    void EndAttack()
    {
        _player._playermovement.SetVelocity(Vector3.zero);
    }

    private void FixedUpdate() {
        float speed =  Movement.ReadValue<Vector2>().magnitude;
        _animator.SetFloat("Speed",speed,0.15f,Time.fixedDeltaTime);
        //print(speed);
    }
}
