using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerPicker : MonoBehaviour
{
    public event Action OnFindPickableObject;
    public event Action OnLostPickableObject;
    public event Action<bool> OnEnterPoint;
    public event Action OnExitPoint;
    public event Action<bool> OnHoldingChanged;

    private bool _isHolding = false;

    [SerializeField] private float _throwingForce; 

    private IPickable _clothestPickable;
    private IPickable _currentPickable;

    [SerializeField] private Transform _pickedItemPosition;

    private MainInput _input;

    private Point _currentPoint;

    [Inject] 
    private void Initialize(MainInput input)
    {
        _input = input;
    }

    private void OnEnable()
    {
        _input.Player.PickUp.performed += HandlePickingUp;
        _input.Player.ThrowOut.performed += HandleThrowOut;
        _input.Player.PickUp.performed += HandlePoint;
    }

    private void OnDisable()
    {
        _input.Player.PickUp.performed -= HandlePickingUp;
        _input.Player.ThrowOut.performed -= HandleThrowOut;
        _input.Player.PickUp.performed -= HandlePoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IPickable>(out var pickable))
        {
            OnFindPickableObject?.Invoke();

            _clothestPickable = pickable;
        }

        if (other.TryGetComponent<Point>(out var point))
        {
            _currentPoint = point;

            OnEnterPoint?.Invoke(point.IsEmpty);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IPickable>(out var pickable) && IsObjectReachable(other.transform))
        {
            OnLostPickableObject?.Invoke();

            _clothestPickable = null;
        }

        if (other.TryGetComponent<Point>(out var point))
        {
            _currentPoint = null;

            OnExitPoint?.Invoke();
        }
    }

    private void HandlePickingUp(InputAction.CallbackContext context)
    {
        if (_isHolding || _clothestPickable is null)
        {
            return;
        }

        _isHolding = true;
        OnHoldingChanged?.Invoke(_isHolding);
        OnLostPickableObject?.Invoke();

        _clothestPickable.PickUp(_pickedItemPosition);
        _currentPickable = _clothestPickable;
    }

    private void HandlePoint(InputAction.CallbackContext context)
    {
        if (_currentPoint is null)
        {
            return;
        }

        if (_currentPickable is not null && _currentPoint.IsEmpty)
        {
            _currentPoint.PutPickable(_currentPickable);

            _isHolding = false;
            OnHoldingChanged?.Invoke(_isHolding);
            OnLostPickableObject?.Invoke();
            _currentPickable = null;
            _clothestPickable = null;
        }
        else if (_currentPickable is null && !_currentPoint.IsEmpty)
        {
            _currentPickable = _currentPoint.GetPickable(_pickedItemPosition);

            _isHolding = true;
            OnHoldingChanged?.Invoke(_isHolding);
        }
    }

    private void HandleThrowOut(InputAction.CallbackContext context)
    {
        if (!_isHolding)
        {
            return;
        }

        _isHolding = false;
        OnHoldingChanged?.Invoke(_isHolding);

        _currentPickable.ThrowOut(_throwingForce);
        _currentPickable = null;
    }

    private bool IsObjectReachable(Transform objectTransform)
    {
        bool isReachable = true;

        Vector3 direction = objectTransform.position - transform.position;

        RaycastHit[] hittedObjects = Physics.RaycastAll(transform.position, direction, direction.magnitude);

        foreach (var hit in hittedObjects)
        {
            if (hit.collider.gameObject == gameObject)
            {
                continue;
            } 
            else if (hit.collider.gameObject == objectTransform.gameObject)
            {
                break;
            }
            else if (hit.collider.gameObject != objectTransform.gameObject)
            {
                _isHolding = false;

                break;
            }
        }

        return isReachable;
    }
}