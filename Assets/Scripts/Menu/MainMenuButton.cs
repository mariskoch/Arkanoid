using GameOverScreen;
using Managers;
using TimerUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace Menu
{
    public class MainMenuButton : MonoBehaviour
    {
        private Button _mainMenuButton;

        private void Start()
        {
            _mainMenuButton = GetComponent<Button>();
            _mainMenuButton.onClick.AddListener(() =>
            {
                // save highscore when run is stopped between levels
                if (!PlayerPrefs.HasKey("Highscore") && GameManager.Instance.Score > 0 ||
                    GameManager.Instance.GameState == GameState.Paused &&
                    GameManager.Instance.Score > Highscore.GetHighscore() ||
                    GameManager.Instance.currentLevel != GameManager.Instance.availableLevels &&
                    GameManager.Instance.GameState == GameState.Win &&
                    GameManager.Instance.Score > Highscore.GetHighscore())
                {
                    Highscore.SaveHighscore(GameManager.Instance.Score);
                }
                
                Time.timeScale = 1;
                GameManager.Instance.GameState = GameState.Menu;
                GameManager.Instance.ResetLives();
                GameManager.Instance.currentLevel = 1;
                GameManager.Instance.currentTutorial = 1;
                UIManager.Instance.ResetScore();
                Timer.Instance.ResetTimer();
                SceneManager.LoadScene("MainMenu");
            });
        }
    }
}