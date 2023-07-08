using DG.Tweening;
using UnityEngine;

namespace GMTK_2023.UI.Elements
{
    public enum TransitionType
    {
        None, Fade, Custom
    }

    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UIViewBase : MonoBehaviour
    {
        public bool IsShowed => m_isShowed;

        [SerializeField] private TransitionType m_transitionType;
        [SerializeField] protected float m_transitionDuration;

        private bool m_isShowed = false;
        private Canvas m_canvas;
        private CanvasGroup m_canvasGroup;

        public void Show()
        {
            if (m_isShowed)
            {
                return;
            }
            m_isShowed = true;

            switch (m_transitionType)
            {
                case TransitionType.None:
                    EnableCanvas();
                    break;
                case TransitionType.Fade:
                    PlayFadeIn();
                    break;
                case TransitionType.Custom:
                    m_canvas.enabled = true;
                    m_canvasGroup.blocksRaycasts = false;

                    PlayCustomTransitionIn();
                    break;
            }
        }

        public void Hide()
        {
            if (!m_isShowed)
            {
                return;
            }
            m_isShowed = false;

            switch (m_transitionType)
            {
                case TransitionType.None:
                    DisableCanvas();
                    break;
                case TransitionType.Fade:
                    PlayFadeOut();
                    break;
                case TransitionType.Custom:
                    m_canvas.enabled = true;
                    m_canvasGroup.blocksRaycasts = false;

                    PlayCustomTransitionOut();
                    break;
            }
        }

        protected void EnableCanvas()
        {
            m_canvas.enabled = true;
            m_canvasGroup.blocksRaycasts = true;
        }

        protected void DisableCanvas()
        {
            m_canvas.enabled = false;
            m_canvasGroup.blocksRaycasts = false;
        }

        protected virtual void PlayCustomTransitionIn()
        {
            EnableCanvas();
        }

        protected virtual void PlayCustomTransitionOut()
        {
            DisableCanvas();
        }

        protected virtual void Awake()
        {
            m_canvas = GetComponent<Canvas>();
            m_canvasGroup = GetComponent<CanvasGroup>();

            DisableCanvas();
        }

        private void PlayFadeIn()
        {
            m_canvas.enabled = true;
            m_canvasGroup.blocksRaycasts = false;

            m_canvasGroup.DOFade(1f, m_transitionDuration)
                .From(0f)
                .onComplete += EnableCanvas;
        }

        private void PlayFadeOut()
        {
            m_canvas.enabled = true;
            m_canvasGroup.blocksRaycasts = false;

            m_canvasGroup.DOFade(0f, m_transitionDuration)
                .From(1f)
                .onComplete = DisableCanvas;
        }
    }
}
