using Item.Realization;
using Player.Inventory.Systems;
using Setup.Player;
using UI.Player;
using UnityEngine;
using Utiles.Raycasters;
using Utiles.Storage;

namespace Player.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private int _storageCapacity;

        [Space, SerializeField] private CameraTowardsScreenPointRaycaster _checker;

        [Space, SerializeField] private Transform _root;

        private ItemStorage<BaseItem> _itemsStorage;

        private PickSystem _pickSystem;
        private ThrowSystem _throwSystem;
        
        public void Initialize(BaseInput input, AttackButton attackButton, PlayerSetup setup)
        {
            _itemsStorage = new ItemStorage<BaseItem>(_storageCapacity);

            _pickSystem = new PickSystem(_itemsStorage, input, _root, _checker);
            _throwSystem = new ThrowSystem(_itemsStorage, attackButton, setup);
        }

        private void OnDestroy()
        {
            _itemsStorage?.Dispose();
            _pickSystem?.Dispose();
            _throwSystem?.Dispose();
        }
    }
}