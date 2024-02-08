using System;

namespace Behaviours
{
    public class Timer
    {
        public float RemainingSeconds {get; private set;}

        public Timer(float duration)
        {
            RemainingSeconds = duration;
        }
        public event Action OnTimerEnd;

        public void Tick(float deltaTime)
        {
            if(RemainingSeconds == 0f){return;}
            RemainingSeconds -= deltaTime;
        }

        void CheckTimerEnd()
        {
            if(RemainingSeconds>0f){return;}
            RemainingSeconds = 0f;

            OnTimerEnd?.Invoke(); //if OnTimerEnd is not null then invoke.
        }
    }
}