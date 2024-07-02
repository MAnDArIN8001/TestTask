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

        transform.position = picker.position;
        transform.parent = picker.transform;

        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
    }

    public void ThrowOut(float throwingForce)
    {
        ChangeCollidersState(true);

        Transform parent = transform.parent;

        transform.parent = null;
        _rigidbody.useGravity = true;
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
