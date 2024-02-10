using Characters;
using UnityEngine;

public class G_Enemy : MonoBehaviour
{
    private Character Base; // Character Core Functionality
    private ICharacter SelfRole;

    G_Enemy()
    {
        Base = new();
        SelfRole = new Enemy(Base);
    }


    public void Die()
    {
        SelfRole.Die();
    }

    public void Heal(float healAmount)
    {
        SelfRole.Heal(healAmount);
    }

    public void TakeDamage(float damageTaken)
    {
        SelfRole.TakeDamage(damageTaken);
    }

    private void Start()
    {
        
        print($"Enemy initiated with health: {SelfRole.health.GetRemainingHealth()}");
    }

    private void Update()
    {
        
    }
}
