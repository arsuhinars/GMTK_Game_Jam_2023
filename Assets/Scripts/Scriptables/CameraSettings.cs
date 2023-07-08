using UnityEngine;

namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "Game/Controllers/Camera Settings")]
    public class CameraSettings : ScriptableObject
    {
        public Vector3 lookDirection;
        public float lookDistance;
        public float moveSmoothTime;
        //public float moveSpeed;
        // TODO: replace with moving along fish group
        //public Vector2 moveDirection;
        [Space]
        public float boundsExtraSpace;
    }
}
