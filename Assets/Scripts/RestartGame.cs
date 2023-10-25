using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    public Button button;
    
    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            GameManager.Instance.ResetLives();
            GameManager.Instance.currentLevel = 1;
            SceneManager.LoadScene("Level1");
        });
    }
}
