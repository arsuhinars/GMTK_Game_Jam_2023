using GMTK_2023.Scriptables;
using GMTK_2023.Utils;
using UnityEngine;

namespace GMTK_2023.Behaviours
{
    [RequireComponent(typeof(SphereCollider))]
    public class ForceField : MonoBehaviour
    {
        public Vector3 Direction
        {
            get => m_dir;
            set
            {
                m_dir = value.normalized;
                transform.rotation = Quaternion.LookRotation(m_dir);
            }
        }
        public float Radius => m_collider.radius;

        [SerializeField] private ForceFieldSettings m_settings;
        private SphereCollider m_collider;
        private Vector3 m_dir = Vector3.zero;

        private float forceMultiplier=1.0f;

        public void setForceMultiplier(float multiplier)
        {
            forceMultiplier=multiplier;
        }

        private void Awake()
        {
            m_collider = GetComponent<SphereCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            ForceApplyingRoutine(other);
        }

        private void OnTriggerStay(Collider other)
        {
            ForceApplyingRoutine(other);
        }

        private void ForceApplyingRoutine(Collider collider)
        {
            if (!TagUtils.CompareTagsFromArray(collider, m_settings.tags))
            {
                return;
            }

            var rb = collider.attachedRigidbody;
            if (rb == null)
            {
                return;
            }

            float forceFactor = Mathf.Clamp01(
                Vector3.Distance(
                    rb.position, m_collider.transform.TransformPoint(m_collider.center)
                ) / m_collider.radius
            );
            float forceVal = m_settings.centerForceValue
                + (m_settings.borderForceValue - m_settings.centerForceValue) * forceFactor * forceMultiplier;

            rb.AddForce(forceVal * m_dir);
        }
    }
}
