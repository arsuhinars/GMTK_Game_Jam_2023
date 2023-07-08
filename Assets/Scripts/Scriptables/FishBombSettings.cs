using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "FishBombSettings", menuName = "Game/Behaviours/Fish Bomb Settings")]
    public class FishBombSettings : ScriptableObject
    {
        public float m_bombTimeCountdown=5f;
        public float m_launchVelocity=100f;

        [SerializeField] public GameObject bombPrefab;
    }
}
