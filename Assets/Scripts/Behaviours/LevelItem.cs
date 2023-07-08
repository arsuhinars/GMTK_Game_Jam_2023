using GMTK_2023.Controllers;
using UnityEngine;

namespace GMTK_2023.Behaviours
{
    [RequireComponent(typeof(Rigidbody))]
    public class LevelItem : PoolItem, ISpawnable
    {
        public bool IsAlive => m_isAlive;

        private bool m_isAlive = false;
        private Rigidbody m_rb;

        public void Spawn()
        {
            m_isAlive = true;
            gameObject.SetActive(true);
            m_rb.velocity = Vector3.zero;
        }

        public void Kill()
        {
            if (!m_isAlive)
            {
                return;
            }

            m_isAlive = false;
            gameObject.SetActive(false);
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
    }
}
