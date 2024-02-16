using UnityEngine;
using Behaviours;
using Characters;
using MyBox;

class PlayerCommands : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    internal Movement _playermovement;
    internal Vector2 InputDirection = Vector2.zero;

    

    G_Enemy enemyTarget;

    public void StartAttack()
    {
        _animator.Play("Attack");

        float min = Mathf.Infinity;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<G_Enemy>(out G_Enemy target))
            {
                float distance = (target.transform.position - transform.position).magnitude;
                if (distance < min)
                {
                    min = distance;
                    enemyTarget = target;
                }
            }
        }
        if (enemyTarget != null)
        {
            Vector3 movementVelocity = (enemyTarget.transform.position - transform.position) * 6f;
            _playermovement.SetVelocity(new Vector3(movementVelocity.x, 0, movementVelocity.z));
            _playermovement.PlayerController.transform.forward = movementVelocity;
            Invoke("EndAttack", 0.2f);
        }
    }
    private void EndAttack()
    {
        _playermovement.SetVelocity(Vector3.zero);
        if (enemyTarget != null)
        {
            enemyTarget.TakeDamage(21f);
            enemyTarget = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

    public void lookAround()
    { 

    }

    public void Jump()
    {
        print("Jumped");
        _playermovement.Jump(1f);
        if (_playermovement.IsGrounded)
        {
            _animator.SetTrigger("Jump");
        }
    }

    private void FixedUpdate()
    {
        _playermovement.Move(Time.fixedDeltaTime, InputDirection);
        float animationSpeed = InputDirection.magnitude ;
        _animator.SetFloat("Speed", animationSpeed, 0.2f, Time.fixedDeltaTime);
        _animator.SetBool("OnGround", _playermovement.IsGrounded);
    }
}
