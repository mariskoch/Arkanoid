using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private Button _playButton;

    private void Start()
    {
        _playButton = GetComponent<Button>();
        _playButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Level1");
        });
    }
}
