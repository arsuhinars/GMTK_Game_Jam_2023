using GMTK_2023.Managers;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK_2023.UI.Views
{
    public class ActiveGameView : UIViewBase
    {
        [SerializeField] private TextMeshProUGUI m_scoreText;
        [SerializeField] private Button m_pauseButton;

        private StringBuilder m_scoreTextBuilder = new();

        protected override void Start()
        {
            base.Start();
            m_pauseButton.onClick.AddListener(OnPauseClick);
            GameManager.Instance.Score.OnValueChanged += UpdateScoreText;
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Score.OnValueChanged -= UpdateScoreText;
            }
        }

        private void UpdateScoreText(int oldValue, int newValue)
        {
            m_scoreTextBuilder.Clear();
            m_scoreTextBuilder.Append("Score - ");
            m_scoreTextBuilder.Append(newValue);

            m_scoreText.text = m_scoreTextBuilder.ToString();
        }

        private void OnPauseClick()
        {
            GameManager.Instance.PauseGame();
        }
    }
}
