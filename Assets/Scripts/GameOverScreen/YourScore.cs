using TMPro;
using UnityEngine;

namespace GameOverScreen
{
    public class YourScore : MonoBehaviour
    {
        private TextMeshProUGUI _yourScoreText;

        private void Start()
        {
            _yourScoreText = GetComponent<TextMeshProUGUI>();
            ShowScore();
        }

        private void ShowScore()
        {
            var score = GameManager.Instance.Score;
            var displayScore = score.ToString("D5");
            _yourScoreText.text = $"Your Score: {displayScore}";
        }
    }
}