using GMTK_2023.Behaviours;
using GMTK_2023.Controllers;
using GMTK_2023.Scriptables;
using GMTK_2023.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace GMTK_2023.Managers
{
    public class FishManager : MonoBehaviour
    {
        public static FishManager Instance { get; private set; } = null;

        public float MinGroupRadius => m_settings.minFishGroupRadius;
        public float MaxGroupRadius => m_settings.maxFishGroupRadius;
        public FishEntity Leader => m_leader;

        [SerializeField] private FishManagerSettings m_settings;
        [SerializeField] private Transform m_spawnRoot;
        private FishEntity m_leader;
        private LinkedList<FishEntity> m_slaves = new();
        private ObjectPool<PoolItem> m_pool;

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
            GameManager.Instance.OnStart += OnGameStart;

            m_pool = ObjectPoolFactory.CreatePrefabsPool(m_settings.prefab, m_spawnRoot);
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

            if (m_pool.CountActive == 0)
            {
                GameManager.Instance.EndGame(GameEndReason.Died);
                return;
            }

            var it = m_slaves.First;
            while (it != null)
            {
                if (!it.Value.IsAlive)
                {
                    var t = it;
                    it = it.Next;
                    m_slaves.Remove(t);
                    continue;
                }
                
                it = it.Next;
            }

            if (!m_leader.IsAlive)
            {
                m_leader = m_slaves.Last.Value;
                m_slaves.RemoveLast();
            }
        }

        private void OnGameStart()
        {
            if (m_leader != null && m_leader.IsActiveInPool)
            {
                m_pool.Release(m_leader);
            }

            foreach (var fish in m_slaves)
            {
                m_pool.Release(fish);
            }

            m_pool.Clear();
            m_slaves = new();

            var pos = new Vector3(
                0f,
                LevelManager.Instance.WaterLevelY - m_settings.fishSwimDepth,
                0f
            );

            m_leader = m_pool.Get() as FishEntity;
            m_leader.transform.position = pos;
            m_leader.Spawn();

            for (int i = 0; i < m_settings.initialFishCount - 1; ++i)
            {
                var offset = RandomUtils.GetRandomVectorInRadius(
                    m_settings.minFishGroupRadius, m_settings.maxFishGroupRadius
                );

                var fish = m_pool.Get() as FishEntity;
                fish.transform.position = pos + offset;
                fish.Spawn();

                m_slaves.AddLast(fish);
            }
        }
    }
}
