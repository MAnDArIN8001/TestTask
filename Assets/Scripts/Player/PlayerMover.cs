using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    public event Action<Vector2> OnMovementCompute;
    public event Action<bool> OnGroundChanged;

    private bool _isOnGround;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Vector2 _lastMovementValue;

    private Rigidbody _rigidbody;

    private MainInput _input;

    [Inject] 
    private void Initialize(MainInput input)
    {
        _input = input;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Jump.performed += HandleJump;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Jump.performed -= HandleJump;
    }

    private void Update()
    {
        Vector2 movementDirection = ReadInputValues();

        Move(movementDirection);

        if (movementDirection != _lastMovementValue)
        {
            OnMovementCompute?.Invoke(movementDirection);
        }

        _lastMovementValue = movementDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isOnGround = true;
        OnGroundChanged?.Invoke(_isOnGround);
    }

    private void Move(Vector2 direction)
    {
        Vector3 moveDirection = transform.forward * direction.y * _speed + transform.right * direction.x * _speed;
        moveDirection.y = _rigidbody.velocity.y;

        _rigidbody.velocity = moveDirection;
    }

    private void Jump()
    {
        Vector3 computedVelocity = new Vector3(_rigidbody.velocity.x, _jumpForce, _rigidbody.velocity.z);

        _rigidbody.velocity = computedVelocity;
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if (!_isOnGround)
        {
            return;
        }

        _isOnGround = false;
        OnGroundChanged?.Invoke(_isOnGround);

        Jump();
    }

    private Vector2 ReadInputValues() => _input.Player.Movement.ReadValue<Vector2>();
}
