﻿using System;
using TMPro;
using UnityEngine;

namespace GameOverScreen
{
    public class Highscore : MonoBehaviour
    {
        private TextMeshProUGUI _highScoreText;
        [SerializeField] private TextMeshProUGUI highscoreErrorText;

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

        private int? GetHighscore()
        {
            if (PlayerPrefs.HasKey("Highscore"))
            {
                return PlayerPrefs.GetInt("Highscore");
            }

            return null;
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
    }
}