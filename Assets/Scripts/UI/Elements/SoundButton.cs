using GMTK_2023.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GNT.UI.Elements
{
    [RequireComponent(typeof(Image))]
    public class SoundButton : Selectable
    {
        [SerializeField] private Sprite m_onImage;
        [SerializeField] private Sprite m_offImage;

        private Image m_image;

        protected override void Awake()
        {
            base.Awake();

            m_image = GetComponent<Image>();
        }

        protected override void Start()
        {
            base.Start();

            SoundManager.Instance.IsSoundOn.OnValueChanged += OnSoundStateChange;
            UpdateImage();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.IsSoundOn.OnValueChanged -= OnSoundStateChange;
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            var soundState = SoundManager.Instance.IsSoundOn;
            soundState.Value = !soundState.Value;
        }

        private void OnSoundStateChange(bool oldVal, bool newVal)
        {
            UpdateImage();
        }

        private void UpdateImage()
        {
            bool soundState = SoundManager.Instance.IsSoundOn.Value;
            m_image.sprite = soundState ? m_onImage : m_offImage;
        }
    }
}
