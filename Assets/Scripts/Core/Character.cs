using System;
using Behaviours;

namespace Characters
{
    public interface ICharacter
    {
        Health health { get; set; }
        void TakeDamage(float damageTaken);
        void Heal(float healAmount);
        void Die();
    }
    public class Character : ICharacter
    {
        public Health health { get; set; }

        public Character() {
            health = new Health();
            health.Died += Die;
        }

        ~Character()
        {
            health.Died -= Die;
        }

        public void TakeDamage(float damageTaken)
        {
            health -= damageTaken;
        }

        public void Heal(float healAmount)
        {
            health += healAmount;
        }

        public void Die() 
        {
        }
    }
    public abstract class CharacterDecorator : ICharacter
    {
        protected ICharacter _character;

        public Health health { get; set; }

        public CharacterDecorator(ICharacter character)
        {
            _character = character;
            health = character.health;
        }


        public virtual void TakeDamage(float damageTaken)
        {
            _character.TakeDamage(damageTaken);
        }

        public virtual void Heal(float healAmount)
        {
            _character.Heal(healAmount);
        }

        public  virtual void Die() { 
            _character.Die();
        }
    }


    public class Enemy : CharacterDecorator
    {
        public Enemy(ICharacter character) : base(character)
        {   
        }

        public override void Die()
        {
            UnityEngine.MonoBehaviour.print("Enemy slain!");
        }
    }

    public class Player : CharacterDecorator
    {
        public Player(ICharacter character) : base(character)
        {
        }

        public override void Die()
        {
            UnityEngine.MonoBehaviour.print("Game Over");
        }
    }
}
