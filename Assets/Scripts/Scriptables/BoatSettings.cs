using UnityEngine;

namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "BoatSettings", menuName = "Game/Behaviours/Boat Settings")]
    public class BoatSettings : ScriptableObject
    {
        [Range(0f, 1f)]
        public float bombSpawnChance;
        public float cooldownTime;
        public float throwAngle;
        public Vector3 throwOffset;
    }
}
