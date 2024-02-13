using Characters;
using UnityEngine;
using UnityEngine.UI;

public class G_Enemy : MonoBehaviour
{
    private Character Base; // Character Core Functionality
    private ICharacter SelfRole;

    [SerializeField]
    private Image bar;

    G_Enemy()
    {
        Base = new();
        SelfRole = new Enemy(Base);

    }

    private void ChangeBar()
    {
        bar.fillAmount = SelfRole.health.GetRemainingHealth();
    }

    public void Die()
    {
        SelfRole.Die();
        print("Died a crooked death");
        Destroy(gameObject);

    }

    public void Heal(float healAmount)
    {
        SelfRole.Heal(healAmount);
    }

    public void TakeDamage(float damageTaken)
    {
        print("took damage!");
        SelfRole.TakeDamage(damageTaken);
        ChangeBar();
    }

    private void Start()
    {
        ChangeBar();
        SelfRole.health.Died += Die;
        print($"Enemy initiated with health: {SelfRole.health.GetRemainingHealth()}");
    }

    private void OnDestroy()
    {
        SelfRole.health.Died -= Die;
    }
    private void Update()
    {
        
    }
}
