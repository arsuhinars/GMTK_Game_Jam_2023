using GMTK_2023.Behaviours;
using UnityEngine;
using UnityEngine.Pool;

namespace GMTK_2023.Utils
{
    public class ObjectPoolFactory
    {
        public static ObjectPool<PoolItem> CreatePrefabsPool(PoolItem prefab, Transform parent=null)
        {
            ObjectPool<PoolItem> pool = null;

            PoolItem OnCreate()
            {
                var obj = Object.Instantiate(prefab, parent);
                obj.CurrentPool = pool;
                return obj;
            }

            void OnGet(PoolItem obj)
            {
                obj.gameObject.SetActive(true);
                obj.IsActiveInPool = true;
                obj.OnGet();
            }

            void OnRelease(PoolItem obj)
            {
                obj.gameObject.SetActive(false);
                obj.IsActiveInPool = false;
                obj.OnRelease();
            }

            void OnDestroy(PoolItem obj)
            {
                Object.Destroy(obj.gameObject);
            }

            pool = new ObjectPool<PoolItem>(OnCreate, OnGet, OnRelease, OnDestroy);

            return pool;
        }
    }
}
