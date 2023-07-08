using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "FishBaitSettings", menuName = "Game/Behaviours/Fish Bait Settings")]
    public class FishBaitSettings : ScriptableObject
    {
        public float m_baitTimeCountdown=10f;
        public bool m_baitEnabled=false;
    }
}
