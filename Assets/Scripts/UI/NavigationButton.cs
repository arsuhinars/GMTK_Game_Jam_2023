using GMTK_2023.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GMTK_2023.UI
{
    public class NavigationButton : Selectable, IPointerClickHandler
    {
        [Space]
        [SerializeField]
        private string m_viewName;

        public void OnPointerClick(PointerEventData eventData)
        {
            UIManager.Instance.SetView(m_viewName);
        }
    }
}
