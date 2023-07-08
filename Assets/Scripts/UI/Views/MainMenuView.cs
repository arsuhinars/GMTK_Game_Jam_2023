using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTK_2023.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GMTK_2023.Managers;

namespace GMTK_2023.UI.Views
{
    public class MainMenuView : UIViewBase
    {
        [SerializeField] private Button m_startButton;
        [SerializeField] private Button m_quitButton;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            base.EnableCanvas();

            m_startButton.onClick.AddListener(OnStartClick);
            m_quitButton.onClick.AddListener(OnQuitClick);
        }

        private void OnStartClick()
        {
            SceneManager.LoadScene("GameScene");
        }

        private void OnQuitClick()
        {
            Application.Quit();
        }
    }
}