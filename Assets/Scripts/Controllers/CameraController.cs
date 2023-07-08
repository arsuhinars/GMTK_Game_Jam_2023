using GMTK_2023.Behaviours;
using GMTK_2023.Managers;
using GMTK_2023.Scriptables;
using Unity.VisualScripting;
using UnityEngine;

namespace GMTK_2023.Controllers
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        public Bounds ViewBounds => m_viewBounds;
        public Camera Camera => m_camera;
        public Vector3 MoveDirection => m_settings.moveDirection;

        [SerializeField] private CameraSettings m_settings;
        private Camera m_camera;
        private Vector2 m_normalizedMoveDir;
        private Vector3 m_initialPos;
        private Bounds m_viewBounds;
        private Vector3 m_boundsOffset;

        public Vector3 PlaneRaycast(Vector2 screenPosition)
        {
            float h = LevelManager.Instance.WaterLevelY - transform.position.y;
            var dir = m_camera.ScreenPointToRay(screenPosition).direction;
            return h / dir.y * dir + transform.position;
        }

        private void Awake()
        {
            m_camera = GetComponent<Camera>();
        }

        private void Start()
        {
            m_initialPos = transform.localPosition;
            m_normalizedMoveDir = m_settings.moveDirection.normalized;
            CalculateBounds();

            GameManager.Instance.OnStart += OnGameStart;
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStart -= OnGameStart;
            }
        }

        private void Update()
        {
            if (!GameManager.Instance.IsStarted)
            {
                return;
            }

            var step = m_settings.moveSpeed * Time.deltaTime * m_normalizedMoveDir * 5;
            transform.localPosition += new Vector3(step.x, 0f, step.y);

            m_viewBounds.center = m_boundsOffset + transform.localPosition;
        }

        private void CalculateBounds()
        {
            var topRight = PlaneRaycast(new Vector2(Screen.width, Screen.height));
            var bottomLeft = PlaneRaycast(new Vector2(0f, 0f));

            var max = topRight;
            var min = new Vector3(
                2f * transform.position.x - topRight.x,
                bottomLeft.y,
                bottomLeft.z
            );

            m_viewBounds = new Bounds();

            var extra = Vector3.one * m_settings.boundsExtraSpace;
            m_viewBounds.SetMinMax(min - extra, max + extra);

            m_boundsOffset = m_viewBounds.center - m_initialPos;
        }

        private void OnGameStart()
        {
            transform.localPosition = m_initialPos;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(m_viewBounds.center, m_viewBounds.size);
        }
    }
}
