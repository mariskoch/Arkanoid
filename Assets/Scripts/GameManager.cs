using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance => _instance;

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

    [HideInInspector] public int currentLevel = 1;
    public int availableLevels = 2;
    public int availableLives = 2;
    public GameObject gameOverScreenPrefab;
    public event Action<int> OnLiveReduction;
    public static event Action OnGameOver;
    public int Lives { get; set; }
    public int Score { get; set; }
    public int VolatileScore { get; set; }
    // private variable needed?
    private GameState _gameState;
    public GameState GameState { get; set; }
    private GameObject _gameOverCanvas;
    private float _remainingSlowDuration = 0.0f;

    private void Start()
    {
        //Screen.SetResolution(2560, 1440, true);
        GameState = GameState.ReadyToPlay;
        ResetLives();
        Score = 0;
        Ball.OnBallDeath += OnBallDeath;
    }
    
    // TODO: Remove - for debugging only
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && (GameState == GameState.ReadyToPlay || GameState == GameState.GameRunning))
        {
            PlayerPrefs.SetInt("Highscore", 0);
        }
        if (Input.GetKeyDown(KeyCode.L) && (GameState == GameState.ReadyToPlay || GameState == GameState.GameRunning))
        {
            currentLevel = availableLevels - 1;
            LoadNextLevel();
        }
    }
    */
    
    private void OnBallDeath(Ball ball)
    {
        if (BallManager.Instance.Balls.Count <= 0)
        {
            this.Lives--;

            OnLiveReduction?.Invoke(Lives);

            if (this.Lives <= 0)
            {
                GameState = GameState.GameOver;
                OnGameOver?.Invoke();
                ShowGameOverScreen();
                Timer.Instance.StopTimer();
            }
            else
            {
                GameState = GameState.ReadyToPlay;
                BallManager.Instance.ResetBalls();
                var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex);
                UIManager.Instance.SetVScore(0);
                VolatileScore = 0;
                Timer.Instance.StopTimer(true);
            }
        }
    }

    private void ShowGameOverScreen()
    {
        UIManager.Instance.SetVScore(0);
        VolatileScore = 0;
        _gameOverCanvas = Instantiate(gameOverScreenPrefab);
        _gameOverCanvas.SetActive(true);
    }

    private void HideGameOverScreen()
    {
        if (_gameOverCanvas != null)
        {
            _gameOverCanvas.SetActive(false);
        }
    }

    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;
    }

    public void ResetLives()
    {
        Lives = availableLives;
        UIManager.Instance.SetLives(availableLives);
    }

    public void LoadNextLevel()
    {
        if (currentLevel >= availableLevels)
        {
            return;
        }

        Timer.Instance.ResetTimer();
        currentLevel++;
        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void ChangeGameSpeedForDuration(float speed, float duration)
    {
        if (Mathf.Approximately(Time.timeScale, speed) && _remainingSlowDuration > 0.0f)
        {
            _remainingSlowDuration += duration;
            return;
        } else if (!Mathf.Approximately(Time.timeScale, speed) && _remainingSlowDuration > 0.0f)
        {
            ChangeGameToSpeed(speed);
            _remainingSlowDuration = duration;
            return;
        }
        ChangeGameToSpeed(speed);
        StartCoroutine(ResetSpeedAfterTime(duration));
    }
    
    private void ChangeGameToSpeed(float speed)
    {
        Time.timeScale = speed;
    }

    private IEnumerator ResetSpeedAfterTime(float delay)
    {
        _remainingSlowDuration = delay;
        while (!Mathf.Approximately(_remainingSlowDuration, 0.0f))
        {
            _remainingSlowDuration = Mathf.MoveTowards(_remainingSlowDuration, 0.0f, Time.deltaTime);
            yield return null;
        }
        ChangeGameToSpeed(1.0f);
    }
}