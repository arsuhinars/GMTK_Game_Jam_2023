using GMTK_2023.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK_2023.UI.Views
{
    public class MainView : UIViewBase
    {
        [SerializeField] private Button m_startButton;
        [SerializeField] private Button m_quitButton;

        protected override void Start()
        {
            base.Start();

            m_startButton.onClick.AddListener(OnStartClick);
            m_quitButton.onClick.AddListener(OnQuitClick);
        }

        private void OnStartClick()
        {
            GameManager.Instance.StartGame();
        }

        private void OnQuitClick()
        {
            Application.Quit();
        }
    }
}