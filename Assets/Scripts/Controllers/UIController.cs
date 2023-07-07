using UnityEngine;
using GMTK_2023.Managers;
using UnityEngine.SceneManagement;
using System.Collections;

namespace GMTK_2023.Controllers
{
    public class UIController : MonoBehaviour
    {
        private void Awake() {
            GameManager.Instance.OnStart += UIOnStart;
            GameManager.Instance.OnEnd += UIOnEnd;
            GameManager.Instance.OnPause += UIOnPause;
            GameManager.Instance.OnResume += UIOnStart;
        }
        private void Start()
        {
            StartCoroutine(test());
            
        }

        private IEnumerator test()
        {
            yield return new WaitForSeconds(2f);
            GameManager.Instance.EndGame(GameEndReason.Died);
        }
        

        private void UIOnStart()
        {
            UIManager.Instance.SetView("GameUI");
        }

        private void UIOnPause()
        {
            UIManager.Instance.SetView("PauseUI");
        }

        private void UIOnEnd(GameEndReason reason)
        {
            Debug.Log(reason);
            UIManager.Instance.SetView("GameOverUI");
        }


        #region UIButtons
        public void PauseButton()
        {
            GameManager.Instance.PauseGame();
        }

        public void UnPauseButton()
        {
            GameManager.Instance.ResumeGame();
        }

        public void QuitButton()
        {
            Application.Quit();
        }

        public void Retry()
        {
            GameManager.Instance.StartGame();
        }
        #endregion
        
    }
}
