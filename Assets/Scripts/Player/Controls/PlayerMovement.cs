using Setup.Player;
using UnityEngine;

namespace Player.Controls
{
    public class PlayerMovement : MonoBehaviour
    {
        private Vector2 _lastInput;
        private Vector3 _direction;
        
        private BaseInput _input;
        private Joystick _joystick;
        
        private PlayerSetup _setup;

        [SerializeField] private CharacterController _characterController;

        public void Initialize(BaseInput input, Joystick joystick, PlayerSetup setup)
        {
            _input = input;
            _joystick = joystick;
            _setup = setup;
        }

        private void Update()
        {
            var input = ReadInputValues();

            if (input != _lastInput)
            {
                _direction = ConvertInputToDirection(input);
            }

            _characterController.SimpleMove(_direction * _setup.MovementSpeed);

            _lastInput = input;
        }

        private Vector2 ReadInputValues() => _joystick.Direction;

        private Vector3 ConvertInputToDirection(Vector2 input)
        {
            var direction = transform.forward * input.y + transform.right * input.x;
            direction.y = 0;

            return direction;
        }
    }
}