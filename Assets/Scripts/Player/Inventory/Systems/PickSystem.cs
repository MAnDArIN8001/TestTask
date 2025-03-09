using System;
using Item.Realization;
using UnityEngine;
using UnityEngine.InputSystem;
using Utiles.Raycasters;
using Utiles.Storage;

namespace Player.Inventory.Systems
{
    public class PickSystem : IDisposable
    {
        private readonly Transform _root;

        private readonly BaseInput _baseInput;
        
        private readonly ItemStorage<BaseItem> _itemsStorage;

        private readonly CameraTowardsScreenPointRaycaster _raycaster;

        public PickSystem(ItemStorage<BaseItem> itemsStorage, 
            BaseInput baseInput, 
            Transform root,
            CameraTowardsScreenPointRaycaster raycater)
        {
            _itemsStorage = itemsStorage;
            _baseInput = baseInput;
            _root = root;
            _raycaster = raycater;
            
            if (_baseInput is not null)
            {
                _baseInput.Controls.Click.performed += HandleClick;
            }
        }

        private void HandleClick(InputAction.CallbackContext context)
        {
            if (_itemsStorage.IsFull)
            {
                return;
            }

            var clickPosition = _baseInput.Controls.ClickPosition.ReadValue<Vector2>();
            
            var hitInfo = _raycaster.ThrowRayTowardsScreenPoint(clickPosition);
            
            if (hitInfo.collider is null || !hitInfo.collider.TryGetComponent<BaseItem>(out var item))
            {
                return;
            }

            if (_itemsStorage.TryAddItem(item))
            {
                item.PickUp(_root);
            }
        }

        public void Dispose()
        {
            if (_baseInput is not null)
            {
                _baseInput.Controls.Click.performed -= HandleClick;
            }
        }
    }
}