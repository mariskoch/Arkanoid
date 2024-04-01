using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance => _instance;

        private TextMeshProUGUI _livesText;
        private TextMeshProUGUI _scoreText;
        private TextMeshProUGUI _volatileScore;
        private static UIManager _instance;

        private void Awake()
        {
            _livesText = GameObject.Find("Lives").GetComponent<TextMeshProUGUI>();
            _scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
            _volatileScore = GameObject.Find("VScore").GetComponent<TextMeshProUGUI>();

            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void Start()
        {
            SetLives(GameManager.Instance.Lives);
            SetScore(GameManager.Instance.Score);

            GameManager.Instance.OnLiveReduction += SetLives;
            if (GameManager.AreLivesAndScoreCounted()) Brick.OnBrickDestruction += UpdateScore;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnLiveReduction -= SetLives;
            if (GameManager.AreLivesAndScoreCounted()) Brick.OnBrickDestruction -= UpdateScore;
        }

        public void SetLives(int lives)
        {
            var displayString = $"Lives:{Environment.NewLine}{lives}";
            _livesText.text = displayString;
        }

        private void UpdateScore(Brick brick)
        {
            var pointsForBrick = (brick.OriginalHitPoints * 10);
            GameManager.Instance.VolatileScore += pointsForBrick;
            SetVScore(GameManager.Instance.VolatileScore);
        }

        public void SetVScore(int vScore)
        {
            var displayNumber = vScore.ToString("D5");
            var displayString = $"+{displayNumber}";
            _volatileScore.text = displayString;
        }

        public void SetScore(int score)
        {
            // GameManager.Instance.Score += GameManager.Instance.VolatileScore;
            var displayNumber = score.ToString("D5");
            var displayString = $"Score:{Environment.NewLine}{displayNumber}";
            _scoreText.text = displayString;
            GameManager.Instance.VolatileScore = 0;
            // ResetScore();
        }

        public void ResetScore()
        {
            SetScore(0);
            SetVScore(0);
            GameManager.Instance.Score = 0;
            GameManager.Instance.VolatileScore = 0;
        }

        public void SaveScore()
        {
            GameManager.Instance.Score +=
                (int)(GameManager.Instance.VolatileScore * Multiplier.Instance.MultiplierValue);
            GameManager.Instance.VolatileScore = 0;
            SetScore(GameManager.Instance.Score);
            SetVScore(0);
        }
    }
}