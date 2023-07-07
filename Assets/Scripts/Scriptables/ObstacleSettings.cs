using UnityEngine;

namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "ObstacleSettings", menuName = "Game/Behaviours/Obstacle Settings")]
    public class ObstacleSettings : ScriptableObject
    {
        public string playerTag;
    }
}
