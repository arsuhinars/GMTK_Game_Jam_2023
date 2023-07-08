using UnityEngine;

namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "BombSettings", menuName = "Game/Behaviours/Bomb Settings")]
    public class BombSettings : ScriptableObject
    {
        public string fishTag;
        public float explosionRadius;
        public float throwSpeed;
    }
}
