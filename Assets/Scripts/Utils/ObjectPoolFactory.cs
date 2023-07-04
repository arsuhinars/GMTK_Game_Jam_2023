using UnityEngine;
using UnityEngine.Pool;

namespace GMTK_2023.Utils
{
    public class ObjectPoolFactory
    {
        public static IObjectPool<T> CreatePrefabsPool<T>(T prefab, Transform parent=null) where T : Component
        {
            return new ObjectPool<T>(
                // On create
                () =>
                {
                    return Object.Instantiate(prefab, parent);
                },
                // On get
                (T obj) =>
                {
                    obj.gameObject.SetActive(true);
                },
                // On release
                (T obj) =>
                {
                    obj.gameObject.SetActive(false);
                },
                // On destroy
                (T obj) =>
                {
                    Object.Destroy(obj);
                }
            );
        }
    }
}
