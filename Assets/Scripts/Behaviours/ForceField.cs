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
            set => m_dir = value.normalized;
        }

        [SerializeField] private ForceFieldSettings m_settings;
        private SphereCollider m_collider;
        private Vector3 m_dir = Vector3.zero;
        

        private void Awake()
        {
            m_collider = GetComponent<SphereCollider>();
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

            Rigidbody rb = collider.attachedRigidbody;
            if (rb == null) return;

            Vector3 dir = (rb.position - transform.position).normalized;
            dir.y = 0;
            // we need the square magnitude of the direction so the force applied is the same regardless of its distance to the object!!!
            float dist = Vector3.Distance(rb.position,transform.position);
            

            rb.AddForce(dir * (5f - dist) * Time.deltaTime, ForceMode.Impulse);
            //because objects closer to the center should have a stronger force we need to subtract the distance from a base force before multiplying the direction.
        }
    }
}
