using UnityEngine;
using GMTK_2023.Managers;
using GMTK_2023.UI;

namespace GMTK_2023.Controllers
{
    public class UIController : MonoBehaviour
    {
        public PlayerClickableArea ClickableArea => m_clickableArea;

        [SerializeField] private PlayerClickableArea m_clickableArea;

        private void Start()
        {
            GameManager.Instance.OnEnterMenu += OnEnterMenu;
            GameManager.Instance.OnStart += OnGameStart;
            GameManager.Instance.OnEnd += OnGameEnd;
            GameManager.Instance.OnPause += OnGamePause;
            GameManager.Instance.OnResume += OnGameResume;
        }

        private void OnEnterMenu()
        {
            UIManager.Instance.SetView("MainMenu");
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
