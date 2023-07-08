using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "Fish Entity Settings", menuName = "Game/Behaviours/Fish Entity Settings")]
    public class FishEntitySettings : ScriptableObject
    {
        public float wanderTimer;
        public float m_speed;
        public float m_rotationSpeed;
    }
}
