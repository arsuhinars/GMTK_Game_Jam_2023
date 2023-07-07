using UnityEngine;

namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "Game/Controllers/Camera Settings")]
    public class CameraSettings : ScriptableObject
    {
        public float moveSpeed;
        // TODO: replace with moving along fish group
        public Vector2 moveDirection;
    }
}
