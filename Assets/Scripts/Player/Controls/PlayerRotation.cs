using Setup.Player;
using UnityEngine;
using UnityEngine.Serialization;
using Utiles.Constraints;

namespace Player.Controls
{
    public class PlayerRotation : MonoBehaviour
    {
        private float _currentCameraRotationAngel;

        [SerializeField] private Range _verticalViewRange;

        [Space, SerializeField] private Transform _cameraRoot;
        
        private PlayerSetup _setup;

        private BaseInput _baseInput;

        public void Initialize(BaseInput input, PlayerSetup setup)
        {
            _baseInput = input;
            _setup = setup;
        }

        public void Update()
        {
            var input = ReadInputValue();
            
            RotateVertical(input);
            RotateHorizontal(input);
        }
        
        private void RotateHorizontal(Vector2 input)
        {
            var sensitivity = _setup.HorizontalSensitivity;

            float targetYRotation = transform.eulerAngles.y + input.x * sensitivity;
            float smoothedYRotation = Mathf.LerpAngle(transform.eulerAngles.y, targetYRotation, Time.deltaTime * 10f);
                
            transform.rotation = Quaternion.Euler(0, smoothedYRotation, 0);
        }

        private void RotateVertical(Vector2 input)
        {
            var sensitivity = _setup.VerticalSensitivity;
            
            _currentCameraRotationAngel += (-input.y) * sensitivity * Time.deltaTime;
            _currentCameraRotationAngel = Mathf.Clamp(_currentCameraRotationAngel, _verticalViewRange.MinValue, _verticalViewRange.MaxValue);

            _cameraRoot.localRotation = Quaternion.Euler(_currentCameraRotationAngel, 0, 0);
        }

        private Vector2 ReadInputValue() => _baseInput.Controls.Rotation.ReadValue<Vector2>();
    }
}