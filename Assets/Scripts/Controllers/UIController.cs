using UnityEngine;
using GMTK_2023.Managers;

namespace GMTK_2023.Controllers
{
    public class UIController : MonoBehaviour
    {
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
            UIManager.Instance.SetView("MainMenuUI");
        }

        private void OnGameStart()
        {
            UIManager.Instance.SetView("GameUI");
        }

        private void OnGamePause()
        {
            UIManager.Instance.SetView("PauseUI");
        }

        private void OnGameEnd(GameEndReason reason)
        {
            UIManager.Instance.SetView("GameOverUI");
        }

        private void OnGameResume()
        {
            UIManager.Instance.SetView("GameUI");
        }
    }
}
