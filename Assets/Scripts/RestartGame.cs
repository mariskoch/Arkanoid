using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

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
            SceneManager.LoadScene("Level1");
            Timer.Instance.ResetTimer();
        });
    }
}
