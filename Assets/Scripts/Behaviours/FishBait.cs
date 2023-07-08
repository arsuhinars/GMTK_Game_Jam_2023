using GMTK_2023.Scriptables;
using UnityEngine;

namespace GMTK_2023.Behaviours
{
    [RequireComponent(typeof(SphereCollider))]
    public class FishBait : LevelItem
    {
        public float Radius => m_collider.radius;

        [SerializeField] private FishBaitSettings m_settings;
        private SphereCollider m_collider;

        protected override void Awake()
        {
            base.Awake();
            m_collider = GetComponent<SphereCollider>();
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

