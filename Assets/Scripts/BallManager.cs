using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class BallManager : MonoBehaviour
{
    #region Singleton

    private static BallManager _instance;

    public static BallManager Instance => _instance;

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

    public Ball ballPrefab;
    public float initialBallSpacingToPaddle;
    public float initialBallSpeed;
    private Ball _initialBall;
    private Rigidbody2D _initialBallRb;
    public List<Ball> Balls { get; set; }

    private void Start()
    {
        InitBall();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameRunning && !GameManager.Instance.IsGameOver)
        {
            Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
            _initialBall.transform.position =
                new Vector3(paddlePosition.x, paddlePosition.y + initialBallSpacingToPaddle, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.IsGameRunning)
        {
            GameManager.Instance.GameState = GameState.GameRunning;
            Timer.Instance.StartTimer();
            _initialBallRb.isKinematic = false;
            _initialBallRb.AddForce(new Vector2(0, initialBallSpeed));
            GameManager.Instance.IsGameRunning = true;
        }
    }

    private void InitBall()
    {
        Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + initialBallSpacingToPaddle, 0);
        _initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        _initialBallRb = _initialBall.GetComponent<Rigidbody2D>();
        this.Balls = new List<Ball>()
        {
            _initialBall
        };
    }

    public void ResetBalls()
    {
        foreach (var ball in this.Balls.ToList())
        {
            Destroy(ball.gameObject);
        }

        this.InitBall();
    }
}
