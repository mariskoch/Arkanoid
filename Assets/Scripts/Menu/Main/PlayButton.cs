using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace Menu.Main
{
    public class PlayButton : MonoBehaviour
    {
        private Button _playButton;

        private void Start()
        {
            _playButton = GetComponent<Button>();
            _playButton.onClick.AddListener(() =>
            {
                if (GameManager.Instance)
                {
                    GameManager.Instance.GameState = GameState.ReadyToPlay;
                }

                SceneManager.LoadScene("Level1");
            });
        }
    }
}