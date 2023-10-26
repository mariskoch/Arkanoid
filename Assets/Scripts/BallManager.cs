using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    private Ball initialBall;
    private Rigidbody2D initialBallRb;
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
            initialBall.transform.position =
                new Vector3(paddlePosition.x, paddlePosition.y + initialBallSpacingToPaddle, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.IsGameRunning)
        {
            Timer.Instance.StartTimer();
            initialBallRb.isKinematic = false;
            initialBallRb.AddForce(new Vector2(0, initialBallSpeed));
            GameManager.Instance.IsGameRunning = true;
        }
    }

    private void InitBall()
    {
        Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + initialBallSpacingToPaddle, 0);
        initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        initialBallRb = initialBall.GetComponent<Rigidbody2D>();
        this.Balls = new List<Ball>()
        {
            initialBall
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
