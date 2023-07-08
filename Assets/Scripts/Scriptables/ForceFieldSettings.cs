using UnityEngine;

namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "ForceFieldSettings", menuName = "Game/Behaviours/Force Field Settings")]
    public class ForceFieldSettings : ScriptableObject
    {
        public float centerForceValue;
        public float borderForceValue;
        [Tooltip("On objects with that tags force will influence")]
        public string[] tags;
    }
}
