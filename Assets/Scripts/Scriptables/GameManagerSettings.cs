using UnityEngine;

namespace GMTK_2023.Scriptables
{
    [CreateAssetMenu(fileName = "GameManagerSettings", menuName = "Game/Managers/Game Manager Settings")]
    public class GameManagerSettings : ScriptableObject
    {
        public float secondsForScore = 0f;
        [Space]
        public string mainMenuScene;
    }
}
