using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EmptySpaceRotationSystem : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform _rectTransform;
    
    private BaseInput _input;

    public Vector2 Delta { get; private set; }

    public void Initialize(BaseInput input)
    {
        _input = input;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (IsPointerOverEmptySpace(eventData))
        {
            Delta = _input.Controls.Rotation.ReadValue<Vector2>();
        }
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsPointerOverEmptySpace(eventData))
        {
            Delta = Vector2.zero;
        }
    }
    
    private bool IsPointerOverEmptySpace(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
        
        return _rectTransform.rect.Contains(localPoint);
    }
}
