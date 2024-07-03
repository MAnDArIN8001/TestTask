using System;
using UnityEngine;

public class Point : MonoBehaviour
{
    public event Action<Point> OnPointAdded;
    public event Action<Point> OnPointRemoved;

    private IPickable _currentPickable = null;

    public bool IsEmpty => _currentPickable is null;

    public void PutPickable(IPickable pickable)
    {
        if (_currentPickable is not null)
        {
            return;
        }

        pickable.PickUp(transform);

        _currentPickable = pickable;

        OnPointAdded?.Invoke(this);
    }

    public IPickable GetPickable(Transform picker)
    {
        if (_currentPickable is null)
        {
            return null;
        }

        IPickable temp = _currentPickable;

        _currentPickable.PickUp(picker);
        _currentPickable = null;

        OnPointRemoved?.Invoke(this);

        return temp;
    }
}
