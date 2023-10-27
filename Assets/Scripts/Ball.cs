using System;
using UnityEngine;
using Utils;

public class Ball : MonoBehaviour
{
    [SerializeField] private float verticalSupportThreshold = 0.5f;
    [SerializeField] private float supportImpulse = 0.5f;
    private Rigidbody2D _rb;
    
    public static event Action<Ball> OnBallDeath;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.Instance.GameState != GameState.GameRunning) return;
        var velocity = _rb.velocity;
        if (Math.Abs(velocity.y) <= verticalSupportThreshold)
        {
            var isMovingUp = velocity.y > 0;
            _rb.velocity = new Vector2(velocity.x, isMovingUp ? supportImpulse : -supportImpulse);
        }
    }

    public void Die()
    {
        OnBallDeath?.Invoke(this);
        Destroy(gameObject, 1);
    }
}

