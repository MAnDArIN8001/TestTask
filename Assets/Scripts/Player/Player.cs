using Player.Controls;
using Setup.Player;
using UnityEngine;
using Zenject;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [Header("Configuration")] 
        [SerializeField] private PlayerSetup _setup;
        
        [Header("Systems")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerRotation _playerRotation;

        [Inject]
        private void Initialize(BaseInput input, Joystick joystick)
        {
            _playerMovement.Initialize(input, joystick, _setup);
            _playerRotation.Initialize(input, _setup);
        }
    }
}