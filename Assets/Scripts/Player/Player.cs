using Player.Controls;
using Player.Inventory;
using Setup.Player;
using UI.Player;
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

        [Space, SerializeField] private InventoryController _inventoryController;

        [Inject]
        private void Initialize(BaseInput input, 
            Joystick joystick, 
            AttackButton attackButton, 
            EmptySpaceRotationSystem rotationSystem)
        {
            _playerMovement.Initialize(input, joystick, _setup);
            _playerRotation.Initialize(rotationSystem, _setup);
            _inventoryController.Initialize(input, attackButton, _setup);
        }
    }
}