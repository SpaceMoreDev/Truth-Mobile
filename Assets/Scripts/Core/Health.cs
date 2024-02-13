using System;

namespace Behaviours
{
    public class Health
    {
        public Action Died;

        private float healthMax = 100.0f;
        private float healthpoints = 100.0f;
        private float healthRemaining 
        {
            set
            {
                if (healthpoints <= healthMax 
                    && healthpoints > 0)
                {
                    healthpoints = value;
                    if (healthpoints <= 0f)
                    { Died?.Invoke(); }
                }
            }
            get { return healthpoints; }
        }

        public Health(float initialHealth)
        {
            healthRemaining = Math.Clamp(initialHealth, 0.0f, healthMax);
        }
        public Health()
        {
            healthRemaining = healthMax;
        }

        public static Health operator +(Health health, float amount)
        {
            health.healthRemaining += amount;
            return health;
        }

        public static Health operator -(Health health, float amount)
        {
            health.healthRemaining -= amount;
            return health;
        }

        public float GetRemainingHealth()
        {
            return healthRemaining/100;
        }

    }
}