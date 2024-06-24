using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenInputHandler : MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    public event Action<Vector2> OnPointerDownEvent;
    public event Action<Vector2> OnPointerUpEvent;
    public event Action<Vector2> OnPointerMoveEvent;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent?.Invoke(eventData.position);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        OnPointerMoveEvent?.Invoke(eventData.position);
    }
}
