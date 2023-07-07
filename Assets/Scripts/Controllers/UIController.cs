using UnityEngine;
using GMTK_2023.Managers;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

namespace GMTK_2023.Controllers
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText, soundButtonOne, soundButtonTwo;
        private bool soundOn = true;


        private void Awake() {
            GameManager.Instance.OnStart += UIOnStart;
            GameManager.Instance.OnEnd += UIOnEnd;
            GameManager.Instance.OnPause += UIOnPause;
            GameManager.Instance.OnResume += UIOnStart;
            GameManager.Instance.OnScore += UIUpdateScore;
        }

        private void UIUpdateScore(int newScore)
        {
            scoreText.text = "Points - " + newScore.ToString();
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
            scoreText.text = "Points - 0";
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

        public void SoundButton()
        {
            if(soundOn){
                soundOn = false;
                soundButtonOne.text = "Sound Off";
                soundButtonTwo.text = "Sound Off";
                //do some Game Manager / Audio Manager volume stuff here.
            }
            else {
                soundOn = true;
                soundButtonOne.text = "Sound On";
                soundButtonTwo.text = "Sound On";
                //do some Game Manager / Audio Manager volume stuff here.
            }
        }

        public void StartGame()
        {
            GameManager.Instance.StartGame();
        }

        public void Retry()
        {
            scoreText.text = "Points - 0";
            GameManager.Instance.StartGame();
        }
        #endregion
        
    }
}
