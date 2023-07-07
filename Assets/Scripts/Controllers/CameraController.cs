using GMTK_2023.Behaviours;
using GMTK_2023.Managers;
using GMTK_2023.Scriptables;
using UnityEngine;

namespace GMTK_2023.Controllers
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        public Camera Camera => m_camera;
        public Vector3 MoveDirection => m_settings.moveDirection;

        [SerializeField] private CameraSettings m_settings;
        private Camera m_camera;
        private Vector2 m_normalizedMoveDir;
        private Vector3 m_initialPos;

        private void Awake()
        {
            m_camera = GetComponent<Camera>();
        }

        private void Start()
        {
            m_initialPos = transform.localPosition;
            m_normalizedMoveDir = m_settings.moveDirection.normalized;

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

            var step = m_settings.moveSpeed * Time.deltaTime * m_normalizedMoveDir;
            transform.localPosition += new Vector3(step.x, 0f, step.y);
        }

        private void OnGameStart()
        {
            transform.localPosition = m_initialPos;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<LevelItem>(out var levelItem))
            {
                levelItem.ReleaseFromPool();
            }
        }
    }
}
