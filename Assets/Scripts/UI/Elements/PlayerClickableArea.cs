using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GMTK_2023.UI.Elements
{
    public class PlayerClickableArea : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Vector2> OnClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(eventData.position);
        }
    }
}
