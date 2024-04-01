using System;
using System.Collections.Generic;
using System.Linq;
using GameEssentials.Paddle;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;
using Random = System.Random;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance => _instance;
    public float BallSpeed { get; private set; } = 8.0f;
    public List<Ball> Balls { get; private set; }
    
    [SerializeField] private float initialBallSpacingToPaddle = 0.4f;
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private int maxAmountOfBalls = 25;
    
    private Ball _initialBall;
    private Rigidbody2D _initialBallRb;
    private Movement _movement;
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
    }

    private void OnDisable()
    {
        _movement.Player.StartGame.performed -= InputActionHandler;
        _movement.Player.StartGame.Disable();
    }
    
    private void InputActionHandler(InputAction.CallbackContext ctx)
    {
        if (GameManager.Instance.GameState == GameState.ReadyToPlay)
        {
            GameManager.Instance.GameState = GameState.GameRunning;
            Timer.Instance.StartTimer();
            _initialBallRb.isKinematic = false;
            _initialBallRb.velocity = new Vector2(0, BallSpeed);
        }
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

    public void SpawnBalls(Vector3 position, int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            if (Balls.Count >= maxAmountOfBalls) return;
            var spawned = Instantiate(ballPrefab, position, Quaternion.identity);
            var spawnedRb = spawned.GetComponent<Rigidbody2D>();
            spawnedRb.isKinematic = false;
            var random = new Random();
            spawnedRb.velocity = new Vector2((float) random.NextDouble(), (float) random.NextDouble()).normalized * BallSpeed;
            Balls.Add(spawned);
        }
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
