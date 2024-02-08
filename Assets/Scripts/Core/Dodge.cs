using System;
using UnityEngine;

namespace Behaviours
{
    public class Dodge
    {
        private Animator _anim;
        private Vector2 _input;
        private CharacterController _controller;
        private Camera _camera;
        private Movement _movement;
        private float dodgeSpeed = 2f;
        private float _tempspeed;
        private bool _isdodging = false;
        public bool IsDodging {get{return _isdodging;}}
        public event Action FinishDodge;

        //constants
        const string PLAYER_DODGE = "Dodge";

        public Dodge(Animator animator, Movement movement, CharacterController controller, float speed)
        {
            _anim = animator;
            _movement = movement;
            _controller = controller;
            _tempspeed = movement.Speed;
            dodgeSpeed = speed;
        }
        public Dodge(Animator animator, Movement movement, CharacterController controller, float speed, Camera camera, Vector2 input)
        {
            _anim = animator;
            _movement = movement;
            _controller = controller;
            _camera = camera;
            _tempspeed = movement.Speed;
            _input = input;
            dodgeSpeed = speed;
        }

        public void StartDodge()
        {
            _anim.SetBool("isDodging", false);
            if(!_isdodging)
            {
                // staminabar.DecreaseStamina(dodgeStamina);

                _anim.SetBool("Shield",false);
                _anim.SetBool("Attack", false);

                if(!_anim.GetBool("isDodging"))
                {
                    _anim.SetBool("isDodging", true);
                    _anim.speed = 1;
                    _movement.Speed = _tempspeed;
                    _movement.IsSprinting = false;
                    _anim.Play(PLAYER_DODGE,0);
                } 
            }
        }
        
        public void Dodging()
        {   
            _movement.CanMove = false;
            if(_input != Vector2.zero){
                Vector3 direction = new Vector3(_input.x, 0, _input.y);
                if(_camera != null)
                    direction = direction.x * _camera.transform.right.normalized + direction.z * _camera.transform.forward.normalized;
                else
                    direction = direction.x * _controller.transform.right.normalized + direction.z * _controller.transform.forward.normalized;
                
                direction.y = 0f;
                _controller.gameObject.transform.forward = direction;
            }

            Vector3 movementVelocity = _controller.gameObject.transform.forward * dodgeSpeed;
            _movement.SetVelocity(new Vector3(movementVelocity.x,3,movementVelocity.z));
            _isdodging = true;
        }
        
        public void DodgeEnd()
        {
            // FinishDodge?.Invoke();
            _movement.SetVelocity(new Vector3(0,_movement.CurrentVelocity.y,0));
            _anim.SetBool("isDodging", false);
            _movement.CanMove = true;
            _isdodging = false;
        }
    }
}
