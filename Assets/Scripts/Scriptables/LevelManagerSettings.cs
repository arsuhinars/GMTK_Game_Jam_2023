using GMTK_2023.Behaviours;
using System;
using UnityEngine;

namespace GMTK_2023.Scriptables
{
    [Serializable]
    public struct LevelPrefab
    {
        public int maxCount;
        public PoolItem prefab;
    }

    [CreateAssetMenu(fileName = "LevelManagerSettings", menuName = "Game/Managers/Level Manager Settings")]
    public class LevelManagerSettings : ScriptableObject
    {
        public float waterLevelY;
        [Space]
        public LevelPrefab[] prefabs;
    }
}
