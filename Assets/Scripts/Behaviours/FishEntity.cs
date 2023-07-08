using UnityEngine;
using GMTK_2023.Scriptables;
using GMTK_2023.Managers;
using System.Drawing;
using GMTK_2023.Utils;
using Unity.VisualScripting;

namespace GMTK_2023.Behaviours
{
    public enum FishState
    {
        FollowingLeader,
        MovingAlongForce,
        MovingToBait
    };

    [RequireComponent(typeof(Rigidbody))]
    public class FishEntity : PoolItem, ISpawnable
    {
        public bool IsAlive => m_isAlive;

        [SerializeField] private FishEntitySettings m_settings;
        private Rigidbody m_rb;
        private bool m_isAlive;
        private Vector3 m_moveDir;
        private bool m_isMovingFast = false;
        private Vector3? m_leaderOffset = null;
        private FishBait m_activeBait = null;
        private ForceField m_activeForce = null;

        public void Spawn()
        {
            m_isAlive = true;
            m_moveDir = RandomUtils.GetRandomVectorInRadius(1f, 1f);
        }

        public void Kill()
        {
            m_isAlive = false;

            ReleaseFromPool();
        }

        public override void OnGet() { }

        public override void OnRelease() { }

        private void Awake()
        {
            m_rb = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(m_settings.forceFieldTag))
            {
                m_activeForce = other.GetComponent<ForceField>();
            }
            else if (other.CompareTag(m_settings.fishBaitTag))
            {
                m_activeBait = other.GetComponent<FishBait>();
            }
        }

        private void FixedUpdate()
        {
            if (!m_isAlive || !GameManager.Instance.IsStarted)
            {
                return;
            }

            if (m_activeBait != null)
            {
                HandleMovingToBait();
            }
            else if (m_activeForce != null)
            {
                HandleActiveForce();
            }
            else if (FishManager.Instance.Leader != null)
            {
                HandleFollowingLeader();
            }
            else
            {
                m_isMovingFast = false;
                //m_moveDir = Vector3.zero;
            }

            MovingRoutine();
        }

        private void HandleFollowingLeader()
        {
            var manager = FishManager.Instance;
            var lead = manager.Leader;
            if (lead == null || lead == this)
            {
                return;
            }

            if (m_leaderOffset == null)
            {
                m_leaderOffset = RandomUtils.GetRandomVectorInRadius(
                    manager.MinGroupRadius, manager.MaxGroupRadius
                );
                return;
            }

            if (MoveToPoint(lead.transform.position + (Vector3)m_leaderOffset))
            {
                m_isMovingFast = false;
                m_moveDir = lead.m_moveDir;
            }
        }

        private void HandleActiveForce()
        {
            if (m_activeForce == null)
            {
                return;
            }

            var dist = Vector3.Distance(transform.position, m_activeForce.transform.position);
            if (dist > m_activeForce.Radius)
            {
                m_activeForce = null;
                return;
            }

            m_isMovingFast = false;
            m_moveDir = m_activeForce.Direction;
        }

        private void HandleMovingToBait()
        {
            if (m_activeBait == null)
            {
                return;
            }

            var dist = Vector3.Distance(transform.position, m_activeBait.transform.position);
            if (!m_activeBait.IsActiveInPool || dist > m_activeBait.Radius * 1.1f)
            {
                m_activeBait = null;
                return;
            }

            MoveToPoint(m_activeBait.transform.position);
        }

        private void MovingRoutine()
        {
            if (Mathf.Approximately(m_moveDir.sqrMagnitude, 0f))
            {
                return;
            }

            var vel = m_moveDir.normalized;
            if (m_isMovingFast)
            {
                vel *= m_settings.fastMoveSpeed;
            }
            else
            {
                vel *= m_settings.moveSpeed;
            }

            m_rb.velocity = vel;
            m_rb.rotation = Quaternion.LookRotation(vel);
        }

        /// <summary>
        /// Move fish towards the point
        /// </summary>
        /// <returns>`true` if fish reaches the point, `false` if don't</returns>
        private bool MoveToPoint(Vector3 point)
        {
            var dist = Vector3.Distance(transform.position, point);
            if (dist > m_settings.minDistanceToInterestedPoint)
            {
                m_isMovingFast = true;
                m_moveDir = point - transform.position;
                return false;
            }

            return true;
        }
    }
}
