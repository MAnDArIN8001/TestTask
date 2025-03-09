using Item.View;
using UnityEngine;

namespace Item.Realization
{
    [RequireComponent(typeof(Rigidbody))]
    public class BaseItem : MonoBehaviour, IBaseItem
    {
        private Rigidbody _rigidbody;

        [SerializeField] private TeleportMove _teleportMove;

        private void Awake()
        {
            if (!gameObject.TryGetComponent<Rigidbody>(out _rigidbody))
            {
                Debug.LogWarning($"The {gameObject} doesnt contains Rigidbody Component");
            }
        }

        public virtual void PickUp(Transform newRoot)
        {
            transform.SetParent(newRoot);
            
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
            
            _teleportMove.TeleportToPoint(newRoot.position);
        }

        public virtual void Throw(Vector3 direction, float force)
        {
            transform.SetParent(null);
            
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            
            _teleportMove.BreakTeleportation();
            
            _rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}