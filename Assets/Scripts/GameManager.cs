using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance => _instance;

    private void Awake()
    {
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

    public bool isGameRunning { get; set; }
    public int AvailableLives = 2;
    [HideInInspector] public int currentLevel = 1;
    [HideInInspector] public int Lives { get; set; }

    private void Start()
    {
        // Screen.SetResolution(450, 437, false);
        this.Lives = AvailableLives;
        Ball.OnBallDeath += OnBallDeath;
    }

    private void OnBallDeath(Ball ball)
    {
        if (BallManager.Instance.Balls.Count <= 0)
        {
            this.Lives--;

            if (this.Lives <= 0)
            {
                // show game over screen
            }
            else
            {
                // reset ball
                // stop the game
                // reload the scene

                BallManager.Instance.ResetBalls();
            }
        }
    }
}
