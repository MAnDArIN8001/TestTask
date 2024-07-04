using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class FreeCameraZone : MonoBehaviour
{
    public event Action OnPlayerEnterZone;
    public event Action OnPlayerLeaveZone;

    private bool _isPlayerInZone;

    private CameraZoneState _currentState;
        
    [SerializeField] private GameObject _mainVirtualCamera;
    [SerializeField] private GameObject _topDownCamera;

    private PlayerMover _currentMover;

    private MainInput _input;

    [Inject]
    private void Initialize(MainInput input)
    {
        _input = input;
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.PickUp.performed += HandleCommunication;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.PickUp.performed += HandleCommunication;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMover>(out var mover))
        {
            _currentMover = mover;

            OnPlayerEnterZone?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerMover>(out var mover))
        {
            _currentMover = null;

            OnPlayerLeaveZone?.Invoke();
        }
    }

    private void HandleCommunication(InputAction.CallbackContext context)
    {
        if (_currentMover is null)
        {
            return;
        } 

        _currentState = _currentState == CameraZoneState.Default ? CameraZoneState.InZoom : CameraZoneState.Default;

        _currentMover.enabled = _currentState == CameraZoneState.Default;
        _input.Enable();

        _mainVirtualCamera.SetActive(_currentState == CameraZoneState.Default);
        _topDownCamera.SetActive(_currentState == CameraZoneState.InZoom);
    }
}
