using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton

    private static UIManager _instance;

    public static UIManager Instance => _instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        livesText = GameObject.Find("Lives").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        
        GameManager.Instance.OnLiveReduction += SetLives;
        Brick.OnBrickDestruction += UpdateScore;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLiveReduction -= SetLives;
        Brick.OnBrickDestruction -= UpdateScore;
    }

    public void SetLives(int lives)
    {
        var displayString = $"Lives:{Environment.NewLine}{lives}";
        livesText.text = displayString;
    }

    private void UpdateScore(Brick brick)
    {
        var pointsForBrick = (brick.OriginalHitPoints * 10);
        GameManager.Instance.Score += pointsForBrick;
        SetScore(GameManager.Instance.Score);
    }

    private void SetScore(int score)
    {
        var displayNumber = score.ToString("D5");
        var displayString = $"Score:{Environment.NewLine}{displayNumber}";
        scoreText.text = displayString;
    }
    
    public void ResetScore()
    {
        SetScore(0);
        GameManager.Instance.Score = 0;
    }
}
