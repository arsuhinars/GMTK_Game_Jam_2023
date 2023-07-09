using GMTK_2023.Managers;
using GMTK_2023.Scriptables;
using UnityEngine;

namespace GMTK_2023.Behaviours
{
    [RequireComponent(typeof(SphereCollider))]
    public class FishBait : LevelItem
    {
        public float Radius => m_triggerSphere.radius;

        [SerializeField] private FishBaitSettings m_settings;
        private SphereCollider m_triggerSphere;

        public void Throw(Vector3 origin, Vector3 direction)
        {
            Rigidbody.useGravity = true;
            Rigidbody.position = origin;
            Rigidbody.AddForce(direction.normalized * m_settings.throwSpeed, ForceMode.Impulse);
        }

        protected override void Awake()
        {
            base.Awake();
            m_triggerSphere = GetComponent<SphereCollider>();
        }

        private void FixedUpdate()
        {
            if (!IsAlive)
            {
                return;
            }

            bool isBelowWater = Rigidbody.position.y <= LevelManager.Instance.WaterLevelY;
            m_triggerSphere.enabled = isBelowWater;
            if (isBelowWater)
            {
                Rigidbody.velocity = Vector3.zero;
                Rigidbody.useGravity = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(m_settings.fishTag))
            {
                float dist = Vector3.Distance(other.transform.position, transform.position);
                if (dist < m_settings.killRadius)
                {
                    other.GetComponent<ISpawnable>().Kill();
                    Kill();
                }
            }
        }
    }
}
