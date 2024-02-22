using GameOverScreen;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class MainMenuButton : MonoBehaviour
{
    private Button _mainMenuButton;

    private void Start()
    {
        _mainMenuButton = GetComponent<Button>();
        _mainMenuButton.onClick.AddListener(() =>
        {
            // save highscore when run is stopped between levels
            if (GameManager.Instance.currentLevel != GameManager.Instance.availableLevels &&
                GameManager.Instance.GameState == GameState.Win &&
                GameManager.Instance.Score > Highscore.GetHighscore())
            {
                Highscore.SaveHighscore(GameManager.Instance.Score);
            }

            GameManager.Instance.GameState = GameState.Menu;
            GameManager.Instance.ResetLives();
            GameManager.Instance.currentLevel = 1;
            UIManager.Instance.ResetScore();
            Timer.Instance.ResetTimer();
            SceneManager.LoadScene("MainMenu");
        });
    }
}