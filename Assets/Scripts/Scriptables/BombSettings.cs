using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "BombSettings", menuName = "Game/Behaviours/Bomb Settings")]
    public class BombSettings : ScriptableObject
    {
        public float radius=4f;
    }
}
