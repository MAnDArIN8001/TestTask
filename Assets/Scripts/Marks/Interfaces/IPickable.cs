using UnityEngine;

public interface IPickable
{
    public void PickUp(Transform picker);

    public void ThrowOut(float throwingForce);
}
