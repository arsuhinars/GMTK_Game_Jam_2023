using GMTK_2023.Behaviours;
using GMTK_2023.Controllers;
using GMTK_2023.Scriptables;
using GMTK_2023.Utils;
using UnityEngine;
using UnityEngine.Pool;

namespace GMTK_2023.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; } = null;

        [SerializeField] private LevelManagerSettings m_settings;
        [SerializeField] private Transform m_levelRoot;
        [SerializeField] private Transform m_boat;
        // boat can be used as root to spawn objects
        private CameraController m_camera;
        private ObjectPool<PoolItem>[] m_pools;

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

            m_camera = FindObjectOfType<CameraController>();
        }

        private void Start()
        {
            CreatePools();

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

            int objectsCount = 0;
            for (int i = 0; i < m_pools.Length; i++)
            {
                objectsCount += m_pools[i].CountActive;
            }

            if (objectsCount < m_settings.maxActiveObjects)
            {
                SpawnObject();
            }
        }

        private void OnGameStart()
        {
            for (int i = 0; i < m_pools.Length; ++i)
            {
                m_pools[i].Clear();
            }
        }

        private void CreatePools()
        {
            var prefabs = m_settings.prefabs;

            m_pools = new ObjectPool<PoolItem>[prefabs.Length];
            for (int i = 0; i < prefabs.Length; ++i)
            {
                m_pools[i] = ObjectPoolFactory.CreatePrefabsPool(prefabs[i], m_levelRoot);
            }
        }

        private void SpawnObject()
        {
            // TODO: spawn position depends on camera look direction

            int poolIdx = Random.Range(0, m_pools.Length);

            var obj = m_pools[poolIdx].Get();

            //var camPos = m_levelRoot.InverseTransformPoint(m_camera.transform.position);
            Vector3 spawnPoint = m_boat.position;
            spawnPoint.y = 0f;

            //if we want to have spawned objects 'float' up to the surface we can change y 
            // to ~-3 or something and adjust their script to rise until they are > 0

            float angle = Random.Range(0f, 2f * Mathf.PI);
            var p = m_settings.spawnRadius * new Vector3(
                Mathf.Cos(angle), 0f, Mathf.Sin(angle)
            );

            obj.transform.localPosition = spawnPoint + p;
        }
    }
}
