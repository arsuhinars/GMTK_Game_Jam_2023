using GMTK_2023.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace GMTK_2023.Managers
{
    public enum SoundEffect
    {
        UIClick
    }

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; } = null;

        public ValueObserver<bool> IsSoundOn => m_isSoundOn;

        [SerializeField] private Transform m_audioSourceRoot;
        [SerializeField] private AudioSource m_audioSourcePrefab;
        [SerializeField] private AudioClip m_uiClickEffect;
        [Space]
        [SerializeField] private AudioSource[] m_sceneSources;

        private ValueObserver<bool> m_isSoundOn = new();
        private ObjectPool<AudioSource> m_pool;
        private LinkedList<AudioSource> m_activeSources = new();

        public void PlaySound(SoundEffect effect)
        {
            switch (effect)
            {
                case SoundEffect.UIClick:
                    PlaySound(m_uiClickEffect);
                    break;
            }
        }

        public void PlaySound(AudioClip clip)
        {
            if (!m_isSoundOn.Value)
            {
                return;
            }

            var source = m_pool.Get();
            source.clip = clip;
            source.Play();
            m_activeSources.AddLast(source);
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }
        }

        private void Start()
        {
            m_isSoundOn.Value = PlayerPrefs.GetInt("IsSoundOn", 1) != 0;
            m_isSoundOn.OnValueChanged += (oldVal, newVal) =>
            {
                PlayerPrefs.SetInt("IsSoundOn", newVal ? 1 : 0);
                UpdateSceneSources();
            };

            m_pool = new ObjectPool<AudioSource>(
                () => Instantiate(m_audioSourcePrefab, m_audioSourceRoot),
                (source) => { },
                (source) => source.Stop(),
                (source) => Destroy(source.gameObject)
            );

            UpdateSceneSources();
        }

        private void Update()
        {
            var it = m_activeSources.First;
            while (it != null)
            {
                var t = it.Value;
                if (!t.isPlaying)
                {
                    var oldIt = it;
                    it = it.Next;
                    m_pool.Release(t);
                    m_activeSources.Remove(oldIt);
                    continue;
                }

                it = it.Next;
            }
        }

        private void UpdateSceneSources()
        {
            foreach (var src in m_sceneSources)
            {
                if (m_isSoundOn.Value)
                {
                    src.Play();
                }
                else
                {
                    src.Pause();
                }
            }
        }
    }
}
