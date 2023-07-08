using GMTK_2023.Behaviours;
using GMTK_2023.Controllers;
using GMTK_2023.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace GMTK_2023.Managers
{
    public class WaterManager : MonoBehaviour
    {
        public static WaterManager Instance { get; private set; } = null;

        public RectInt TileBounds => m_tileBounds;

        [SerializeField] private WaterMeshGenerator m_prefab;
        [SerializeField] private Transform m_waterRoot;

        private CameraController m_camera;
        private Dictionary<Vector2Int, WaterMeshGenerator> m_grid;
        private ObjectPool<PoolItem> m_pool;
        private RectInt m_tileBounds;

        public void RemoveMesh(Vector2Int tilePos)
        {
            if (!m_grid.TryGetValue(tilePos, out var mesh))
            {
                return;
            }

            m_pool.Release(mesh);
            m_grid.Remove(tilePos);
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
            GameManager.Instance.OnStart += OnGameStart;

            m_camera = ControllersFacade.Instance.CameraController;
            m_pool = ObjectPoolFactory.CreatePrefabsPool(m_prefab, m_waterRoot);
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

            float waterSize = m_prefab.Size;

            var minBound = m_camera.ViewBounds.min;
            var maxBound = m_camera.ViewBounds.max;

            m_tileBounds.SetMinMax(
                new Vector2Int(
                    Mathf.FloorToInt(minBound.x / waterSize),
                    Mathf.FloorToInt(minBound.z / waterSize)
                ),
                new Vector2Int(
                    Mathf.CeilToInt(maxBound.x / waterSize) + 1,
                    Mathf.CeilToInt(maxBound.z / waterSize) + 1
                )
            );

            for (int x = m_tileBounds.min.x; x < m_tileBounds.max.x; ++x)
            {
                for (int y = m_tileBounds.min.y; y < m_tileBounds.max.y; ++y)
                {
                    var tilePos = new Vector2Int(x, y);
                    if (m_grid.ContainsKey(tilePos))
                    {
                        continue;
                    }

                    var mesh = m_pool.Get() as WaterMeshGenerator;
                    mesh.TilePos = tilePos;
                    mesh.transform.position = new Vector3(x, 0f, y) * waterSize;

                    m_grid.Add(tilePos, mesh);
                }
            }
        }

        private void OnGameStart()
        {
            for (int x = m_tileBounds.min.x; x < m_tileBounds.max.x; ++x)
            {
                for (int y = m_tileBounds.min.y; y < m_tileBounds.max.y; ++y)
                {
                    RemoveMesh(new Vector2Int(x, y));
                }
            }

            m_grid = new();
        }
    }
}
