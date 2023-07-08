using Assets.Scripts.Utils;
using GMTK_2023.Behaviours;
using GMTK_2023.Controllers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Managers
{
    public class WaterManager : MonoBehaviour
    {
        public GameObject WaterMesh;
        public WaterMeshGenerator WaterMeshPrefab;

        public GameObject GameCamera;
        private CameraController _cameraController;
        public Dictionary<(float x, float y), bool> _waterGrid;
        private ObjectPool<WaterMeshGenerator> m_pools;
        [SerializeField] private Transform m_levelRoot;


        private float _waterSize = 10f;
        void Start()
        {
            _waterGrid = new Dictionary<(float x, float y), bool>();
            _cameraController = GameCamera.GetComponent<CameraController>();
            m_pools = WaterPoolFactory.CreatePrefabsPool(WaterMeshPrefab, m_levelRoot);
        }

        private void Update()
        {
            var minBound = _cameraController.ViewBounds.min;
            var maxBound = _cameraController.ViewBounds.max;
            var roundedMinBoundX = Mathf.Round(minBound.x / _waterSize);
            var roundedMaxBoundX = Mathf.Round(maxBound.x / _waterSize * 1.2f);
                                                                       
            var roundedMinBoundZ = Mathf.Round(minBound.z / _waterSize);
            var roundedMaxBoundZ = Mathf.Round(maxBound.z / _waterSize * 1.2f);

            for (float i = roundedMinBoundX; i < roundedMaxBoundX; i++)
            {
                for (float j = roundedMinBoundZ; j < roundedMaxBoundZ; j++)
                {
                    if (_waterGrid.TryAdd((i, j), true))
                    {
                        var waterPosition = new Vector3(i * _waterSize / 2, 0, j * _waterSize / 2);
                        m_pools.Get().SetPosition(waterPosition);
                    }
                }
            }
        }
    }
}
