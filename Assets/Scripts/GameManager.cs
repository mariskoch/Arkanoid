using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Screen = UnityEngine.Device.Screen;

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

    [HideInInspector] public bool isGameRunning { get; set; }
    [HideInInspector] public bool isGameOver { get; set; }
    public int AvailableLives = 2;
    [HideInInspector] public int currentLevel = 1;
    [HideInInspector] public int Lives { get; set; }
    public GameObject gameOverScreenPrefab;
    private GameObject gameOverCanvas;
    public int availableLevels = 2;

    private void Start()
    {
        //Screen.SetResolution(2560, 1440, true);
        Lives = AvailableLives;
        Ball.OnBallDeath += OnBallDeath;
    }

    private void OnBallDeath(Ball ball)
    {
        if (BallManager.Instance.Balls.Count <= 0)
        {
            this.Lives--;

            if (this.Lives <= 0)
            {
                isGameRunning = false;
                isGameOver = true;
                ShowGameOverScreen();
            }
            else
            {
                BallManager.Instance.ResetBalls();
                isGameRunning = false;
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex);
            }
        }
    }

    private void ShowGameOverScreen()
    {
        gameOverCanvas = Instantiate(gameOverScreenPrefab);
        gameOverCanvas.SetActive(true);
    }

    private void HideGameOverScreen()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;
    }

    public void ResetLives()
    {
        Lives = AvailableLives;
    }
    
    public void LoadNextLevel()
    {
        isGameRunning = false;
        if (currentLevel >= availableLevels)
        {
            return;
        }
        currentLevel++;
        SceneManager.LoadScene("Level" + currentLevel);
    }
}


