using GMTK_2023.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK_2023.UI.Views
{
    public class PauseView : UIViewBase
    {
        [SerializeField] private Button m_resumeButton;
        [SerializeField] private Button m_retryButton;
        [SerializeField] private Button m_quitButton;

        protected override void Start()
        {
            base.Start();

            m_resumeButton.onClick.AddListener(OnResumeClick);
            m_retryButton.onClick.AddListener(OnRetryClick);
            m_quitButton.onClick.AddListener(OnQuitClick);
        }

        private void OnResumeClick()
        {
            GameManager.Instance.ResumeGame();
        }

        private void OnRetryClick()
        {
            GameManager.Instance.StartGame();
        }

        private void OnQuitClick()
        {
            GameManager.Instance.EnterMenu();
        }
    }
}
