using GMTK_2023.Scriptables;
using GMTK_2023.Utils;
using System;
using System.Collections;
using UnityEngine;

namespace GMTK_2023.Managers
{
    public enum GameState
    {
        None, Started, Paused, Ended
    }

    public enum GameEndReason
    {
        Died, Finished
    }

    public class GameManager : MonoBehaviour
    {
        public event Action OnStart;
        public event Action<GameEndReason> OnEnd;
        public event Action OnPause;
        public event Action OnResume;
        public event Action<int> OnScore;

        public static GameManager Instance { get; private set; } = null;

        public GameState State => m_state;
        public ValueObserver<int> Score => m_score;
        public bool IsStarted => m_state == GameState.Started;

        [SerializeField] private GameManagerSettings m_settings;
        private GameState m_state;
        private ValueObserver<int> m_score = new();
        private float m_lastScoreTime = 0f;

        public void StartGame()
        {
            m_state = GameState.Started;
            m_score.Value = 0;
            Time.timeScale = 1f;

            OnStart?.Invoke();
        }

        public void EndGame(GameEndReason reason)
        {
            m_state = GameState.Ended;

            OnEnd?.Invoke(reason);
        }

        public void PauseGame()
        {
            if (m_state != GameState.Started)
            {
                return;
            }

            m_state = GameState.Paused;
            Time.timeScale = 0f;

            OnPause?.Invoke();
        }

        public void ResumeGame()
        {
            if (m_state != GameState.Paused)
            {
                return;
            }

            m_state = GameState.Started;
            Time.timeScale = 1f;

            OnResume?.Invoke();
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

        private IEnumerator Start()
        {
            // Skip one frame to let components to subscribe on events
            yield return null;
            //StartGame();
        }

        private void Update()
        {
            ScoreCountingRoutine();
        }

        // TODO: decide how score will counts.
        // Currently it counts by game timer.
        private void ScoreCountingRoutine()
        {
            if (!IsStarted)
            {
                return;
            }

            if (Time.time - m_lastScoreTime > m_settings.secondsForScore)
            {
                m_lastScoreTime = Time.time;
                m_score.Value += 1;
                OnScore?.Invoke(m_score.Value);
            }
        }
    }
}
