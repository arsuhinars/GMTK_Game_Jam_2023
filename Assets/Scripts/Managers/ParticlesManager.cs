using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace GMTK_2023.Managers
{
    public enum ParticleType
    {
        Explosion, WaterSplat
    }

    public class ParticlesManager : MonoBehaviour
    {
        public static ParticlesManager Instance { get; private set; } = null;

        [SerializeField] private ParticleSystem m_explosionPrefab;
        [SerializeField] private ParticleSystem m_waterSplatPrefab;
        [Space]
        [SerializeField] private Transform m_root;

        private Dictionary<ParticleType, ObjectPool<ParticleSystem>> m_pools;
        private LinkedList<Tuple<ParticleType, ParticleSystem>> m_activeParticles = new();

        public void PlayParticles(ParticleType type, Vector3 position)
        {
            var particles = m_pools[type].Get();
            particles.transform.position = position;
            m_activeParticles.AddLast(
                new Tuple<ParticleType, ParticleSystem>(type, particles)
            );
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }
        }

        private void Start()
        {
            m_pools = new()
            {
                { ParticleType.Explosion, CreateParticlesPool(m_explosionPrefab) },
                { ParticleType.WaterSplat, CreateParticlesPool(m_waterSplatPrefab) }
            };

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
            var it = m_activeParticles.First;
            while (it != null)
            {
                var t = it.Value;
                if (!t.Item2.isPlaying)
                {
                    var oldIt = it;
                    it = it.Next;
                    m_pools[t.Item1].Release(t.Item2);
                    m_activeParticles.Remove(oldIt);
                    continue;
                }

                it = it.Next;
            }
        }

        private void OnGameStart()
        {
            while (m_activeParticles.First != null)
            {
                var t = m_activeParticles.First.Value;
                m_pools[t.Item1].Release(t.Item2);
                m_activeParticles.RemoveFirst();
            }
        }

        private ObjectPool<ParticleSystem> CreateParticlesPool(ParticleSystem prefab)
        {
            return new ObjectPool<ParticleSystem>(
                () => Instantiate(prefab, m_root),
                (particles) => particles.Play(),
                (particles) => particles.Stop(),
                (particles) => Destroy(particles.gameObject)
            );
        }
    }
}
