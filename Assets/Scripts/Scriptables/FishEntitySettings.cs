using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "Fish Entity Settings", menuName = "Game/Behaviours/Fish Entity Settings")]
    public class FishEntitySettings : ScriptableObject
    {
        public float rotationSmoothTime;
        public float moveSpeed;
        public float fastMoveSpeed;
        public float minDistanceToInterestedPoint;
        [Space]
        public string forceFieldTag;
        public string fishBaitTag;
    }
}
