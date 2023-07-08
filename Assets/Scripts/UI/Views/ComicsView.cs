using GMTK_2023.UI.Elements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GMTK_2023.UI.Views
{
    public class ComicsView : UIViewBase
    {
        [SerializeField] private Button m_startButton;
        [Space]
        [SerializeField] private string m_gameSceneName;

        private void Start()
        {
            m_startButton.onClick.AddListener(OnStartClick);
        }

        private void OnStartClick()
        {
            SceneManager.LoadScene(m_gameSceneName);
        }
    }
}
