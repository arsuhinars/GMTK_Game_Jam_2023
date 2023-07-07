using UnityEngine;
using UnityEngine.Pool;

namespace GMTK_2023.Behaviours
{
    public abstract class PoolItem : MonoBehaviour
    {
        public IObjectPool<PoolItem> CurrentPool { get; set; }
        public bool IsActiveInPool { get; set; } = false;

        public void ReleaseFromPool()
        {
            CurrentPool.Release(this);
        }

        public abstract void OnGet();

        public abstract void OnRelease();
    }
}
