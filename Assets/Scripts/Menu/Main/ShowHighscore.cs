using GameOverScreen;
using TMPro;
using UnityEngine;

namespace Menu.Main
{
    public class ShowHighscore : MonoBehaviour
    {
        private TextMeshProUGUI _highscoreText;

        private void Awake()
        {
            _highscoreText = GetComponent<TextMeshProUGUI>();
            var displayHighscore = Highscore.GetDisplayHighscore();
            _highscoreText.text = $"Highscore: {displayHighscore}";
        }
    }
}