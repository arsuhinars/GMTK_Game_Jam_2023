using UnityEngine;
using GMTK_2023.Managers;
using GMTK_2023.UI;
using GMTK_2023.UI.Elements;

namespace GMTK_2023.Controllers
{
    public class UIController : MonoBehaviour
    {
        public PlayerClickableArea ClickableArea => m_clickableArea;

        [SerializeField] private PlayerClickableArea m_clickableArea;

        private void Start()
        {
            var manager = GameManager.Instance;
            manager.OnStart += OnGameStart;
            manager.OnEnd += OnGameEnd;
            manager.OnPause += OnGamePause;
            manager.OnResume += OnGameResume;
        }

        private void OnDestroy()
        {
            var manager = GameManager.Instance;
            if (manager != null)
            {
                manager.OnStart -= OnGameStart;
                manager.OnEnd -= OnGameEnd;
                manager.OnPause -= OnGamePause;
                manager.OnResume -= OnGameResume;
            }
        }

        private void OnGameStart()
        {
            UIManager.Instance.SetView("ActiveGame");
        }

        private void OnGamePause()
        {
            UIManager.Instance.SetView("Pause");
        }

        private void OnGameEnd(GameEndReason reason)
        {
            UIManager.Instance.SetView("GameOver");
        }

        private void OnGameResume()
        {
            UIManager.Instance.SetView("ActiveGame");
        }
    }
}
