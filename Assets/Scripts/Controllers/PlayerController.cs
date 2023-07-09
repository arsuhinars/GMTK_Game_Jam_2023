using GMTK_2023.Behaviours;
using GMTK_2023.Scriptables;
using GMTK_2023.UI.Elements;
using UnityEngine;

namespace GMTK_2023.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerSettings m_settings;
        [SerializeField] private ForceField m_forceField;

        private CameraController m_camera;
        private PlayerClickableArea m_clickableArea;
        private Vector3 m_dragStartPos;

        private void Start()
        {
            m_forceField.gameObject.SetActive(false);

            m_camera = ControllersFacade.Instance.CameraController;
            m_clickableArea = ControllersFacade.Instance.UIController.ClickableArea;
            m_clickableArea.OnDragStart += OnDragStart;
            m_clickableArea.OnDragStay += OnDrag;
        }

        private void OnDestroy()
        {
            if (m_clickableArea)
            {
                m_clickableArea.OnDragStart -= OnDragStart;
                m_clickableArea.OnDragStay -= OnDrag;
            }
        }

        private void OnDragStart(Vector2 screenPos)
        {
            m_dragStartPos = m_camera.PlaneRaycast(screenPos);

            //m_forceField.gameObject.SetActive(false);
            m_forceField.transform.position = m_dragStartPos;
        }

        private void OnDrag(Vector2 screenPos)
        {
            var pos = m_camera.PlaneRaycast(screenPos);
            bool isDragActive = Vector3.Distance(pos, m_dragStartPos) > m_settings.minDragRadius;

            m_forceField.gameObject.SetActive(isDragActive);
            if (isDragActive)
            {
                m_forceField.Direction = pos - m_dragStartPos;
            }
        }
    }
}
