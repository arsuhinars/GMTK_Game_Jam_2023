using DG.Tweening;
using GMTK_2023.Managers;
using GMTK_2023.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK_2023.UI.Views
{
    public class MainView : UIViewBase
    {
        [SerializeField] private Button m_startButton;
        [SerializeField] private Button m_quitButton;
        [SerializeField] private RectTransform m_movingMenu;

        private Vector2 m_menuInitialPos;

        private void Start()
        {
            m_menuInitialPos = m_movingMenu.anchoredPosition;

            m_startButton.onClick.AddListener(OnStartClick);
            m_quitButton.onClick.AddListener(OnQuitClick);
        }

        protected override void PlayCustomTransitionIn()
        {
            m_movingMenu
                .DOAnchorPos(m_menuInitialPos, m_transitionDuration)
                .From(m_menuInitialPos + Vector2.up * m_movingMenu.sizeDelta.y)
                .SetEase(Ease.OutBounce, 0.5f)
                .onComplete += EnableCanvas;
        }

        protected override void PlayCustomTransitionOut()
        {
            m_movingMenu
                .DOAnchorPos(
                    m_menuInitialPos + Vector2.up * m_movingMenu.sizeDelta.y,
                    m_transitionDuration
                )
                .From(m_menuInitialPos)
                .onComplete += DisableCanvas;
        }

        private void OnStartClick()
        {
            SoundManager.Instance.PlaySound(SoundEffect.UIClick);
            UIManager.Instance.SetView("Comics");
        }

        private void OnQuitClick()
        {
            Application.Quit();
        }
    }
}
