using UnityEngine;

namespace GMTK_2023.Managers
{
    public enum SoundEffect
    {
        Throw, Explosion, Fishrod, WaterSplash
    }

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; } = null;

        public void PlaySound(SoundEffect effect)
        {

        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }
        }

        private void Start()
        {
            GameManager.Instance.OnStart += OnGameStart;
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStart -= OnGameStart;
            }
        }

        private void OnGameStart()
        {

        }
    }
}
