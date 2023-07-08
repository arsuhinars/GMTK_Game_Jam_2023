using GMTK_2023.Behaviours;
using GMTK_2023.Scriptables;
using GMTK_2023.UI;
using UnityEngine;

namespace GMTK_2023.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerSettings m_settings;
        [SerializeField] private ForceField m_forceField;
        private CameraController m_camera;
        private PlayerClickableArea m_clickArea;

        private void Awake()
        {
            m_camera = FindObjectOfType<CameraController>();
            m_clickArea = FindObjectOfType<PlayerClickableArea>();
        }

        private void Start()
        {
            m_forceField.gameObject.SetActive(false);

            m_clickArea.OnClick += OnScreenClick;
        }

        private void OnDestroy()
        {
            if (m_clickArea)
            {
                m_clickArea.OnClick -= OnScreenClick;
            }
        }

        public void OnScreenClick(Vector2 pos)
        {
            m_forceField.gameObject.SetActive(true);

            var point = Raycast(pos);

            if (point != null)
            {
                m_forceField.Direction = Vector3.forward;
                m_forceField.transform.position = (Vector3)point;
            }
            else
            {
                m_forceField.gameObject.SetActive(false);
            }
        }

        private Vector3? Raycast(Vector2 dir)
        {
            if (!Physics.Raycast(
                m_camera.Camera.ScreenPointToRay(dir),
                out var hit,
                Mathf.Infinity,
                m_settings.groundMask
            ))
            {
                return null;
            }

            return hit.point;
        }
    }
}
