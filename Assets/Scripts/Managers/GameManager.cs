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

        public static GameManager Instance { get; private set; } = null;

        public GameState State => m_state;
        public bool IsStarted => m_state == GameState.Started;

        private GameState m_state;

        public void StartGame()
        {
            m_state = GameState.Started;
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
            StartGame();
        }
    }
}
