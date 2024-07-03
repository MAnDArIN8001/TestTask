using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pickable : MonoBehaviour, IPickable
{
    private Rigidbody _rigidbody;

    private Collider[] _colliders;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        GetComponent<Renderer>().material.color = Random.ColorHSV();

        _colliders = GetComponents<Collider>();
    }

    public void PickUp(Transform picker)
    {
        ChangeCollidersState(false);

        transform.SetParent(picker.transform);
        transform.localPosition = Vector3.zero;

        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
    }

    public void ThrowOut(float throwingForce)
    {
        ChangeCollidersState(true);

        Transform parent = transform.parent;

        transform.SetParent(null);
        _rigidbody.isKinematic = false;
        _rigidbody.velocity = parent.forward * throwingForce;
    }

    private void ChangeCollidersState(bool newState)
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = newState;
        }
    }
}
