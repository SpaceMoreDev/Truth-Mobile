using System;
namespace Behaviours
{
    public class Bar
    {
        // ----- private variables -----
        private float _percentage = 1f;
        private float _requirement = 0.1f;
        private bool _regenerating = false;


        // ----- public variables -----
        public float Value {get => _percentage;}
        public float Requirement {get => _requirement; set=> _requirement = value;}
        public bool IsRegenerating {get => _regenerating;}


        // ----- events -----
        public event Action BarIsEmpty;
        public event Action BarIsOverflowing;
        public event Action BarDecreased;
        public event Action BarIncreased;

        public Bar(float requirement)
        {
            _requirement = requirement;
        }

        public void Increase(float quantity)
        {
            if(_percentage < 1)
            {
                _percentage += quantity;
                BarIncreased?.Invoke();
            }
            else
            {
                _regenerating = false;
                BarIsOverflowing?.Invoke();
            }
        }

        public void Decrease(float quantity)
        {
            _percentage -= quantity;
            BarDecreased?.Invoke();
            _regenerating = true;
            
            if(_percentage <= 0){ BarIsEmpty?.Invoke(); }
        }

        public void Regen(float deltaTime, float wait)
        {
            if(_regenerating){
                Increase(deltaTime * wait * 0.3f);
            }
        }
    }
}
