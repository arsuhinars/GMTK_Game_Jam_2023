using GMTK_2023.Behaviours;
using GMTK_2023.Scriptables;
using UnityEngine;

namespace GMTK_2023.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerSettings m_settings;
        [SerializeField] private ForceField m_forceField;

        private void Awake()
        {
            //m_clickArea = FindObjectOfType<PlayerClickableArea>();
        }

        private void Start()
        {
            m_forceField.gameObject.SetActive(false);

            //m_clickArea.OnClick += OnScreenClick;
        }

        private void OnDestroy()
        {
            //if (m_clickArea)
            //{
            //    m_clickArea.OnClick -= OnScreenClick;
            //}
        }

        public void OnScreenClick(Vector2 pos)
        {
            m_forceField.gameObject.SetActive(true);

            var point = ControllersFacade.Instance.CameraController.PlaneRaycast(pos);

            m_forceField.Direction = Vector3.forward;
            m_forceField.transform.position = point;
        }
    }
}
