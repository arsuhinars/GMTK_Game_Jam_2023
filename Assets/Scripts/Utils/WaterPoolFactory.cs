using GMTK_2023.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Utils
{
    public class WaterPoolFactory
    {
        public static ObjectPool<WaterMeshGenerator> CreatePrefabsPool(WaterMeshGenerator prefab, Transform parent = null)
        {
            ObjectPool<WaterMeshGenerator> pool = null;

            WaterMeshGenerator OnCreate()
            {
                var obj = Object.Instantiate(prefab, parent);
                obj.CurrentPool = pool;
                return obj;
            }

            void OnGet(WaterMeshGenerator obj)
            {
                obj.gameObject.SetActive(true);
                obj.IsActiveInPool = true;
            }

            void OnRelease(WaterMeshGenerator obj)
            {
                obj.gameObject.SetActive(false);
                obj.IsActiveInPool = false;
            }

            void OnDestroy(WaterMeshGenerator obj)
            {
                Object.Destroy(obj.gameObject);
            }

            pool = new ObjectPool<WaterMeshGenerator>(OnCreate, OnGet, OnRelease, OnDestroy);

            return pool;
        }
    }
}
