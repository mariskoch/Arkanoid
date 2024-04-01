using Managers;
using TimerUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace GameOverScreen
{
    public class RestartGame : MonoBehaviour
    {
        public Button button;

        private void Start()
        {
            button.onClick.AddListener(() =>
            {
                GameManager.Instance.GameState = GameState.ReadyToPlay;
                GameManager.Instance.ResetLives();
                GameManager.Instance.currentLevel = 1;
                UIManager.Instance.ResetScore();
                Timer.Instance.ResetTimer();
                if (SceneManager.GetActiveScene().name.Contains("Tutorial"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                else
                {
                    SceneManager.LoadScene("Level1");
                }
            });
        }
    }
}