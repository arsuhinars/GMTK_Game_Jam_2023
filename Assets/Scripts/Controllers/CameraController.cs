using GMTK_2023.Managers;
using GMTK_2023.Scriptables;
using UnityEngine;

namespace GMTK_2023.Controllers
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        public Bounds ViewBounds => m_viewBounds;
        public Camera Camera => m_camera;
        public Vector3 MoveDirection => m_velocity;

        [SerializeField] private CameraSettings m_settings;
        private Camera m_camera;
        private Vector3 m_normalizedLookDir;
        private Vector3 m_velocity;
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
            m_normalizedLookDir = m_settings.lookDirection.normalized;

            transform.position = CalculateTargetPosition();
            transform.rotation = Quaternion.LookRotation(m_settings.lookDirection);
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

            transform.position = Vector3.SmoothDamp(
                transform.position,
                CalculateTargetPosition(),
                ref m_velocity,
                m_settings.moveSmoothTime
            );

            m_viewBounds.center = m_boundsOffset + transform.position;
        }

        private Vector3 CalculateTargetPosition()
        {
            var fishLead = FishManager.Instance.Leader;
            var target = Vector3.zero;
            if (fishLead != null && fishLead.IsAlive)
            {
                target = fishLead.transform.position;
            }

            return target - m_normalizedLookDir * m_settings.lookDistance;
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

            m_boundsOffset = m_viewBounds.center - transform.position;
        }

        private void OnGameStart()
        {
            transform.position = CalculateTargetPosition();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(m_viewBounds.center, m_viewBounds.size);
        }
    }
}
