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
    public TextMeshProUGUI volatileScore;

    private void Start()
    {
        livesText = GameObject.Find("Lives").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        volatileScore = GameObject.Find("VScore").GetComponent<TextMeshProUGUI>();
        
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
        GameManager.Instance.VolatileScore += pointsForBrick;
        SetVScore(GameManager.Instance.VolatileScore);
    }

    public void SetVScore(int vScore)
    {
        var displayNumber = vScore.ToString("D5");
        var displayString = $"+{displayNumber}";
        volatileScore.text = displayString;
    }

    public void SetScore(int score)
    {
        // GameManager.Instance.Score += GameManager.Instance.VolatileScore;
        var displayNumber = score.ToString("D5");
        var displayString = $"Score:{Environment.NewLine}{displayNumber}";
        scoreText.text = displayString;
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
        GameManager.Instance.Score += (int)(GameManager.Instance.VolatileScore * Multiplier.Instance.MultiplierValue);
        GameManager.Instance.VolatileScore = 0;
        SetScore(GameManager.Instance.Score);
        SetVScore(0);
    }
}
