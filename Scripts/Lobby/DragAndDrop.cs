using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Drag and drop funcionality for items in inventory
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    float _dampingSpeed = 0.05f;
    RectTransform _rectTransform;
    Vector3 _velocity = Vector3.zero;
    protected Vector3 _beginDragPosition = new Vector3();


    private void Awake()
    {
        _rectTransform = transform as RectTransform;


    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, eventData.position, eventData.pressEventCamera, out var worldPoint))
            _rectTransform.position = Vector3.SmoothDamp(_rectTransform.position, worldPoint, ref _velocity, _dampingSpeed);
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        _beginDragPosition = transform.position;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        _rectTransform.position = _beginDragPosition;
    }

}
