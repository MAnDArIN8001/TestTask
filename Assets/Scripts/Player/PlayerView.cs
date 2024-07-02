using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    private Animator _animator;

    private PlayerMover _playerMover;
    private PlayerPicker _playerPicker;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
        _playerPicker = GetComponent<PlayerPicker>();
    }

    private void OnEnable()
    {
        _playerMover.OnGroundChanged += HandleGroundationChangings;
        _playerMover.OnMovementCompute += HandleMovement;
        _playerPicker.OnHoldingChanged += HandleHoldingingChanged;
    }

    private void OnDisable()
    {
        _playerMover.OnGroundChanged -= HandleGroundationChangings;
        _playerMover.OnMovementCompute -= HandleMovement;
        _playerPicker.OnHoldingChanged -= HandleHoldingingChanged;
    }

    private void HandleMovement(Vector2 movement)
    {
        _animator.SetFloat(PlayerAnimationConsts.WalkAnimatorField, movement.magnitude);
    }

    private void HandleGroundationChangings(bool isOnGround)
    {
        _animator.SetBool(PlayerAnimationConsts.JumpAnimatorField, isOnGround);
    }

    private void HandleHoldingingChanged(bool isHending)
    {
        _animator.SetBool(PlayerAnimationConsts.HoldingAnimatorField, isHending);
    }
}
