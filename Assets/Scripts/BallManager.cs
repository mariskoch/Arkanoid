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
    public float initialBallSpacingToPaddle = 0.4f;
    public float initialBallSpeed = 8.0f;
    private Ball _initialBall;
    private Rigidbody2D _initialBallRb;
    public List<Ball> Balls { get; set; }

    private void Start()
    {
        InitBall();
    }

    private void Update()
    {
        // moving the ball with the paddle before game starts
        if (GameManager.Instance.GameState == GameState.ReadyToPlay && _initialBall != null)
        {
            Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
            _initialBall.transform.position =
                new Vector3(paddlePosition.x, paddlePosition.y + initialBallSpacingToPaddle, 0);
        }

        // starting the game by pressing space
        if (GameManager.Instance.GameState == GameState.ReadyToPlay && Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.GameState = GameState.GameRunning;
            Timer.Instance.StartTimer();
            _initialBallRb.isKinematic = false;
            _initialBallRb.velocity = new Vector2(0, initialBallSpeed);
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

    public void FreezeBalls()
    {
        foreach (var ball in this.Balls.ToList())
        {
            var rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
        }
    }
}
