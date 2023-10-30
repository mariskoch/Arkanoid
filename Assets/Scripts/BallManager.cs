using System;
using System.Collections.Generic;
using System.Linq;
using GameEssentials.Paddle;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance => _instance;
    public float BallSpeed { get; private set; } = 8.0f;
    public List<Ball> Balls { get; private set; }
    
    [SerializeField] private float initialBallSpacingToPaddle = 0.4f;
    [SerializeField] private Ball ballPrefab;
    
    private Ball _initialBall;
    private Rigidbody2D _initialBallRb;
    private Movement _movement;
    private bool _isStartButtonPressed;
    private static BallManager _instance;

    private void Awake()
    {
        _movement = new Movement();
        
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    
    private void Start()
    {
        InitBall();
    }

    private void OnEnable()
    {
        _movement.Player.StartGame.Enable();
        _movement.Player.StartGame.performed += InputActionHandler;
        _movement.Player.StartGame.canceled += InputActionHandler;
    }

    private void OnDisable()
    {
        _movement.Player.StartGame.performed -= InputActionHandler;
        _movement.Player.StartGame.canceled -= InputActionHandler;
        _movement.Player.StartGame.Disable();
    }
    
    private void InputActionHandler(InputAction.CallbackContext ctx)
    {
        _isStartButtonPressed = ctx.ReadValueAsButton();
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
        if (GameManager.Instance.GameState == GameState.ReadyToPlay && _isStartButtonPressed)
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
