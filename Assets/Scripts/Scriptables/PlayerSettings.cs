using UnityEngine;

namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Game/Controllers/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        public float minDragRadius;
    }
}
