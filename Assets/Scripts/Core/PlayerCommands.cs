using UnityEngine;
using Behaviours;

class PlayerCommands : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    internal Movement _playermovement;
    internal Vector2 InputDirection = Vector2.zero;

    public void StartAttack()
    {
        _animator.Play("Attack");

        Vector3 movementVelocity = transform.forward * 6f;
        _playermovement.SetVelocity(new Vector3(movementVelocity.x, 0, movementVelocity.z));
        Invoke("EndAttack", 0.2f);
    }
    private void EndAttack()
    {
        _playermovement.SetVelocity(Vector3.zero);
    }

    public void lookAround()
    { 

    }

    public void Jump()
    {

    }

    private void FixedUpdate()
    {
        _playermovement.Move(Time.fixedDeltaTime, InputDirection);
        float animationSpeed = InputDirection.magnitude;
        _animator.SetFloat("Speed", animationSpeed, 0.15f, Time.fixedDeltaTime);
    }
}
