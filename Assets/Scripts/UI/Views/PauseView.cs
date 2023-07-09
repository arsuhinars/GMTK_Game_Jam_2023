using GMTK_2023.Managers;
using GMTK_2023.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK_2023.UI.Views
{
    public class PauseView : UIViewBase
    {
        [SerializeField] private Button m_resumeButton;
        [SerializeField] private Button m_retryButton;
        [SerializeField] private Button m_quitButton;

        private void Start()
        {
            m_resumeButton.onClick.AddListener(OnResumeClick);
            m_retryButton.onClick.AddListener(OnRetryClick);
            m_quitButton.onClick.AddListener(OnQuitClick);
        }

        private void OnResumeClick()
        {
            SoundManager.Instance.PlaySound(SoundEffect.UIClick);
            GameManager.Instance.ResumeGame();
        }

        private void OnRetryClick()
        {
            SoundManager.Instance.PlaySound(SoundEffect.UIClick);
            GameManager.Instance.StartGame();
        }

        private void OnQuitClick()
        {
            GameManager.Instance.EnterMenu();
        }
    }
}
