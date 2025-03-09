using Setup.Player;
using UnityEngine;
using Utiles.Constraints;

namespace Player.Controls
{
    public class PlayerRotation : MonoBehaviour
    {
        private float _currentCameraRotationAngel;

        [SerializeField] private bool _useVerticalRotation;
        
        [Space, SerializeField] private Range _verticalViewRange;

        [Space, SerializeField] private Transform _cameraRoot;
        
        private PlayerSetup _setup;

        private EmptySpaceRotationSystem _rotationSystem;

        public void Initialize(EmptySpaceRotationSystem rotationSystem, PlayerSetup setup)
        {
            _rotationSystem = rotationSystem;
            _setup = setup;
        }

        public void Update()
        {
            var input = _rotationSystem.Delta;

            if (_useVerticalRotation)
            {
                RotateVertical(input);   
            }
            
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
    }
}