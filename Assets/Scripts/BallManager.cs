using System.Collections.Generic;
using System.Linq;
using GameEssentials.Paddle;
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

    public float BallSpeed { get; private set; } = 8.0f;
    public List<Ball> Balls { get; private set; }
    
    [SerializeField] private float initialBallSpacingToPaddle = 0.4f;
    [SerializeField] private Ball ballPrefab;
    
    private Ball _initialBall;
    private Rigidbody2D _initialBallRb;

    private void Start()
    {
        InitBall();
    }

    private void Update()
    {
        // moving the ball with the paddle before game starts
        if (GameManager.Instance.GameState == GameState.ReadyToPlay && _initialBall != null)
        {
            var paddlePosition = NewPaddleMovement.Instance.gameObject.transform.position;
            _initialBall.transform.position =
                new Vector3(paddlePosition.x, paddlePosition.y + initialBallSpacingToPaddle, 0);
        }

        // starting the game by pressing space
        if (GameManager.Instance.GameState == GameState.ReadyToPlay && Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.GameState = GameState.GameRunning;
            Timer.Instance.StartTimer();
            _initialBallRb.isKinematic = false;
            _initialBallRb.velocity = new Vector2(0, BallSpeed);
        }
    }

    private void InitBall()
    {
        var paddlePosition = NewPaddleMovement.Instance.gameObject.transform.position;
        var startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + initialBallSpacingToPaddle, 0);
        _initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        _initialBallRb = _initialBall.GetComponent<Rigidbody2D>();
        Balls = new List<Ball>()
        {
            _initialBall
        };
    }

    public void ResetBalls()
    {
        foreach (var ball in Balls.ToList())
        {
            Destroy(ball.gameObject);
        }

        this.InitBall();
    }

    public void FreezeBalls()
    {
        foreach (var ball in Balls.ToList())
        {
            var rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
        }
    }
}
