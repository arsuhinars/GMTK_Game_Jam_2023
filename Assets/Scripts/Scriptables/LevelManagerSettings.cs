using GMTK_2023.Behaviours;
using UnityEngine;

namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "LevelManagerSettings", menuName = "Game/Managers/Level Manager Settings")]
    public class LevelManagerSettings : ScriptableObject
    {
        public float spawnRadius;
        public int maxActiveObjects;
        [Space]
        public PoolItem[] prefabs;
    }
}
