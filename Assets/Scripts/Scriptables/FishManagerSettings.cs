using GMTK_2023.Behaviours;
using UnityEngine;

namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "FishManagerSettings", menuName = "Game/Managers/Fish Manager Settings")]
    public class FishManagerSettings : ScriptableObject
    {
        public FishEntity prefab;
        public float fishSwimDepth;
        public int initialFishCount;
        public float maxFishGroupRadius;
        public float minFishGroupRadius;
    }
}
