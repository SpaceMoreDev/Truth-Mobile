using MyBox;
using UnityEngine;

namespace Behaviours{
    public class Movement
    {
        // ----- private variables -----
        private CharacterController _controller;
        private float _speed = 2.0f;
        public float _turningSpeed = 5f;
        private float _gravityValue = 18.81f;
        private bool _grounded = false;
        private Transform _cameraTransform;
        private Vector3 _lerpVelocity = Vector3.zero;
        private Vector3 _currentVelocity = Vector3.zero;
        private bool withCamera = false;
        private bool _canMove = true;

        // ----- public variables -----
        public bool CanMove { get{return _canMove;} set{_canMove = value;}}
        public Vector3 CurrentVelocity { get{return _currentVelocity;}}
        public float Speed { get{return _speed;} set{_speed = value;}}
        public bool IsSprinting = false;



        public Movement(CharacterController controller)
        {
            _controller = controller;
            withCamera = false;
        }

        public Movement(CharacterController controller, float Speed,float GravityValue, Transform CameraTransform, float TurningSpeed)
        {
            _controller = controller;
            _speed = Speed;
            _cameraTransform = CameraTransform;
            _gravityValue = GravityValue;
            _turningSpeed = TurningSpeed;
            withCamera = true;
        }

        /// <summary>
        /// For setting the current velocity for movement.
        /// </summary>
        /// <param name="newVelocity">New velocity value</param>
        public void SetVelocity(Vector3 newVelocity)
        {
            _currentVelocity = newVelocity;
        }
        /// <summary>
        /// Movement behavior function for 2D input.
        /// </summary>
        /// <param name="deltaTime"> Time between each frame </param>
        /// <param name="input"> The 2D direction for movement </param>
        public void Move(float deltaTime, Vector2 input)
        {   if(_canMove)  
            {
                _grounded = _controller.isGrounded;

                if (_grounded && _currentVelocity.y < 0)
                    _currentVelocity.y = 0f;

                Vector3 move;
                move = new Vector3(input.x, 0, input.y);

                if(withCamera) // mainly for player
                    move = move.x * _cameraTransform.right.normalized + move.z * _cameraTransform.forward.normalized;
                else // mainly for NPCs
                    move = move.x * _controller.transform.right.normalized + move.z * _controller.transform.forward.normalized;

                move.y = 0f;
                _controller.Move(move *_speed *deltaTime); //for input
                if (move != Vector3.zero )
                {
                        _controller.transform.forward = Vector3.SmoothDamp(_controller.transform.forward,move,ref _lerpVelocity,  _turningSpeed *deltaTime);
                }
            }
            _currentVelocity.y -= _gravityValue * deltaTime;
            _controller.Move(_currentVelocity * deltaTime); // for gravity
        }

        /// <summary>
        /// Rotate character based on camera and direction.
        /// </summary>
        /// <param name="deltaTime">Time between each frame</param>
        /// <param name="move">movement direction in 3D</param>
        public void Rotate(Vector2 input)
        {    
            if(_canMove)
            {   
                _grounded = _controller.isGrounded;

                if (_grounded && _currentVelocity.y < 0)
                    _currentVelocity.y = 0f;
                
                Vector3 moveDirection = new Vector3(input.x, 0, input.y);
                moveDirection = Quaternion.AngleAxis(_cameraTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;
                moveDirection.Normalize();

                
                if (moveDirection != Vector3.zero)
                {
                   Quaternion lookRotation = Quaternion.LookRotation(moveDirection,Vector3.up);
                   _controller.transform.rotation = Quaternion.RotateTowards(_controller.transform.rotation, lookRotation, _turningSpeed );
                }
            }

            _currentVelocity.y -= _gravityValue *Time.deltaTime;
            _controller.Move(_currentVelocity *Time.deltaTime); // for gravity
        }
    }
}
