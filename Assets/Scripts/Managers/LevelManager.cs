using GMTK_2023.Behaviours;
using GMTK_2023.Controllers;
using GMTK_2023.Scriptables;
using GMTK_2023.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace GMTK_2023.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; } = null;

        public float WaterLevelY => m_settings.waterLevelY;

        [SerializeField] private LevelManagerSettings m_settings;
        [SerializeField] private Transform m_levelRoot;

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

            for (int i = 0; i < m_pools.Length; i++)
            {
                if (m_pools[i].CountActive < m_settings.prefabs[i].maxCount)
                {
                    SpawnObject(i);
                }
            }
        }

        private void OnGameStart()
        {
            StartCoroutine(OnGameStartCorotuine());
        }

        private IEnumerator OnGameStartCorotuine()
        {
            yield return null;
            for (int i = 0; i < m_pools.Length; i++)
            {
                while (m_pools[i].CountActive < m_settings.prefabs[i].maxCount)
                {
                    SpawnObject(i, true);
                }
            }
        }

        private void CreatePools()
        {
            var prefabs = m_settings.prefabs;

            m_pools = new ObjectPool<PoolItem>[prefabs.Length];
            for (int i = 0; i < prefabs.Length; ++i)
            {
                m_pools[i] = ObjectPoolFactory.CreatePrefabsPool(prefabs[i].prefab, m_levelRoot);
            }
        }

        private void SpawnObject(int poolIdx, bool initialSpawn=false)
        {
            var obj = m_pools[poolIdx].Get();

            var camera = ControllersFacade.Instance.CameraController;
            var bounds = camera.ViewBounds;

            Vector3 pos;
            if (initialSpawn)
            {
                // Spawn within view range
                pos = new Vector3(
                    Random.Range(bounds.min.x + 0.01f, bounds.max.x - 0.01f),
                    m_settings.waterLevelY,
                    Random.Range(bounds.min.z + 0.01f, bounds.max.z - 0.01f)
                );
            }
            else if (Random.Range(0, 2) != 0)
            {
                // Spawn on the front/back side
                pos = new Vector3(
                    Random.Range(bounds.min.x + 0.01f, bounds.max.x - 0.01f),
                    m_settings.waterLevelY,
                    camera.MoveDirection.z > 0f ? bounds.max.z : bounds.min.z
                );
            }
            else
            {
                // Spawn on the left/right side
                pos = new Vector3(
                    camera.MoveDirection.x > 0f ? bounds.max.x : bounds.min.x,
                    m_settings.waterLevelY,
                    Random.Range(bounds.min.z + 0.01f, bounds.max.z - 0.01f)
                );
            }

            obj.transform.position = pos;
            obj.transform.rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
        }
    }
}
