using System;
using Item.Realization;
using Setup.Player;
using UI.Player;
using UnityEngine;
using Utiles.Storage;

namespace Player.Inventory.Systems
{
    public class ThrowSystem : IDisposable
    {
        private readonly Camera _camera;
        
        private readonly PlayerSetup _setup;
        
        private readonly AttackButton _attackButton;
        
        private readonly ItemStorage<BaseItem> _itemsStorage;

        public ThrowSystem(ItemStorage<BaseItem> itemsStorage, AttackButton attackButton, PlayerSetup setup)
        {
            _itemsStorage = itemsStorage;
            _attackButton = attackButton;
            _setup = setup;
            _camera = Camera.main;

            if (_itemsStorage is not null)
            {
                _itemsStorage.OnAddItem += HandleAddedItem;
                _itemsStorage.OnRemoveItem += HandleRemoveItem;

                if (!_itemsStorage.IsContainsAnyItem)
                {
                    _attackButton.Hide();
                }
            }

            if (attackButton is not null) 
            {
                attackButton.AddListener(HandleThrowClick);
            }
        }

        private void HandleAddedItem(BaseItem item)
        {
            _attackButton.Show();
        }

        private void HandleRemoveItem(BaseItem item)
        {
            if (!_itemsStorage.IsContainsAnyItem)
            {
                _attackButton.Hide();
            }
        }

        private void HandleThrowClick()
        {
            if (_itemsStorage.TryGetLastItem(out var item))
            {
                _itemsStorage.TryRemoveItem(item);
                
                item.Throw(_camera.transform.forward, _setup.ThrowForce);
            }
        }

        public void Dispose()
        {
            if (_itemsStorage is not null)
            {
                _itemsStorage.OnRemoveItem -= HandleAddedItem;
            }
            
            if (_attackButton is not null) 
            {
                _attackButton.RemoveListener(HandleThrowClick);
            }
        }
    }
}