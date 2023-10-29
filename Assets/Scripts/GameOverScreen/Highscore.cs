using TMPro;
using UnityEngine;
using Utils;

namespace GameOverScreen
{
    public class Highscore : MonoBehaviour
    {
        private TextMeshProUGUI _highScoreText;
        [SerializeField] private TextMeshProUGUI highscoreErrorText;
        [SerializeField] private TextMeshProUGUI newHighscoreText;

        private void Start()
        {
            _highScoreText = GetComponent<TextMeshProUGUI>();
            HandleHighscore();
        }

        private void HandleHighscore()
        {
            var currentScore = GameManager.Instance.Score;
            var highscore = GetHighscore();

            if (highscore is null || currentScore > highscore)
            {
                newHighscoreText.GetComponent<ShowText>()?.Show();
                try
                {
                    SaveHighscore(currentScore);
                    ShowHighscore(currentScore);
                }
                catch (PlayerPrefsException e)
                {
                    ShowHighscore(highscore ?? 0);
                }
            }
            else
            {
                ShowHighscore(highscore.Value);
            }
        }

        private void ShowHighscore(int highscore)
        {
            var displayHighscore = highscore.ToString("D5");
            _highScoreText.text = $"Highscore: {displayHighscore}";
        }

        private void SaveHighscore(int highscore)
        {
            try
            {
                PlayerPrefs.SetInt("Highscore", highscore);
            }
            catch (PlayerPrefsException e)
            {
                highscoreErrorText.GetComponent<ShowText>()?.Show();
                throw;
            }
        }
        
        private static int? GetHighscore()
        {
            if (PlayerPrefs.HasKey("Highscore"))
            {
                return PlayerPrefs.GetInt("Highscore");
            }

            return null;
        }

        public static string GetDisplayHighscore()
        {
            var highscore = Highscore.GetHighscore();
            return highscore.HasValue ? highscore.Value.ToString("D5") : "00000";
        }
    }
}