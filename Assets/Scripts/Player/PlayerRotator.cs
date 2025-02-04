using UnityEngine;
using Zenject;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _rotationSmoothTime;

    private Vector3 _currentRotationVelocity;

    private MainInput _input;

    [Inject] 
    private void Initialize(MainInput input)
    {
        _input = input;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void FixedUpdate()
    {
        Vector2 mouseDelta = ReadMouseDeltaPosition();
        mouseDelta *= _sensitivity;

        Vector3 targetRotation = new Vector3(0, mouseDelta.x, 0);

        Vector3 currentRotation = transform.eulerAngles;
        Vector3 smoothRotation = Vector3.SmoothDamp(
            currentRotation,
            currentRotation + targetRotation,
            ref _currentRotationVelocity,
            _rotationSmoothTime
        );

        transform.eulerAngles = smoothRotation;
    }

    private Vector2 ReadMouseDeltaPosition() => _input.Mouse.MouseDelta.ReadValue<Vector2>().normalized;
}
