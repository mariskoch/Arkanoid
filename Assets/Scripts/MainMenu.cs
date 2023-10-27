using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class MainMenu : MonoBehaviour
{
    private Button _mainMenuButton;

    private void Start()
    {
        _mainMenuButton = GetComponent<Button>();
        _mainMenuButton.onClick.AddListener(() =>
        {
            GameManager.Instance.GameState = GameState.Menu;
            GameManager.Instance.ResetLives();
            GameManager.Instance.currentLevel = 1;
            UIManager.Instance.ResetScore();
            Timer.Instance.ResetTimer();
            SceneManager.LoadScene("MainMenu");
        });
    }
}
