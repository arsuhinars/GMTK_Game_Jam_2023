using DG.Tweening;
using GMTK_2023.Controllers;
using GMTK_2023.Managers;
using UnityEngine;

namespace GMTK_2023.Behaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class LevelItem : PoolItem, ISpawnable
    {
        public Rigidbody Rigidbody => m_rb;
        public bool IsAlive => m_isAlive;

        [SerializeField] private Transform m_floatingModel;
        [SerializeField] private float m_floatAmplitude;
        [SerializeField] private float m_floatPeriod;
        private bool m_isAlive = false;
        private Rigidbody m_rb;
        private Sequence m_tween;

        public void Spawn()
        {
            m_isAlive = true;
            gameObject.SetActive(true);
            m_rb.velocity = Vector3.zero;

            if (m_floatingModel != null)
            {
                m_tween = DOTween.Sequence();
                m_tween.Append(
                    m_floatingModel.DOLocalMoveY(
                        m_floatAmplitude, m_floatPeriod * 0.5f
                    )
                    .From(-m_floatAmplitude)
                    .SetEase(Ease.InOutSine)
                );
                m_tween.Append(
                    m_floatingModel.DOLocalMoveY(
                        -m_floatAmplitude, m_floatPeriod * 0.5f
                    )
                    .From(m_floatAmplitude)
                    .SetEase(Ease.InOutSine)
                );
                m_tween.SetLoops(-1);
            }
        }

        public void Kill()
        {
            if (!m_isAlive)
            {
                return;
            }

            if (m_tween != null)
            {
                m_tween.Kill();
                m_tween = null;
            }

            m_isAlive = false;
            gameObject.SetActive(false);

            if (IsActiveInPool)
            {
                ReleaseFromPool();
            }
        }

        public override void OnGet()
        {
            Spawn();
        }

        public override void OnRelease()
        {
            Kill();
        }

        protected virtual void Awake()
        {
            m_rb = GetComponent<Rigidbody>();
        }

        protected virtual void Start()
        {
            Spawn();

            GameManager.Instance.OnStart += OnGameStart;
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStart -= OnGameStart;
            }
        }

        protected virtual void Update()
        {
            if (!m_isAlive)
            {
                return;
            }

            var cam = ControllersFacade.Instance.CameraController;
            if (!cam.ViewBounds.Contains(transform.position))
            {
                ReleaseFromPool();
            }
        }

        private void OnGameStart()
        {
            ReleaseFromPool();
        }
    }
}
