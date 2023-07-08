using UnityEngine;

namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "FishBaitSettings", menuName = "Game/Behaviours/Fish Bait Settings")]
    public class FishBaitSettings : ScriptableObject
    {
        public string fishTag;
        public float killRadius;
        public float throwSpeed;
    }
}
