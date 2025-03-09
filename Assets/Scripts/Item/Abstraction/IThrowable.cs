using UnityEngine;

namespace Item
{
    public interface IThrowable
    {
        public void Throw(Vector3 direction, float force);
    }
}