using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GMTK_2023.UI.Elements
{
    public class PlayerClickableArea : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public event Action<Vector2> OnDragStart;
        public event Action<Vector2> OnDragStay;
        public event Action<Vector2> OnDragEnd;

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnDragStart?.Invoke(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragStay?.Invoke(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnDragEnd?.Invoke(eventData.position);
        }
    }
}
