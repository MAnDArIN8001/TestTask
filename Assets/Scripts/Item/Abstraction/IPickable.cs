using UnityEngine;

namespace Item
{
    public interface IPickable
    {
        public void PickUp(Transform newRoot);
    }
}