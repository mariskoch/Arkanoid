using System;
using UnityEngine;
using Utils;

public class Ball : MonoBehaviour
{
    [SerializeField] private float verticalSupportThresholdAndSupport = 1.5f;
    private Rigidbody2D _rb;

    public static event Action<Ball> OnBallDeath;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        this.gameObject.layer = LayerMask.NameToLayer("NoCollisionBallLayer");
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("NoCollisionBallLayer"), LayerMask.NameToLayer("NoCollisionBallLayer"), true);
    }

    private void Update()
    {
        if (GameManager.Instance.GameState != GameState.GameRunning) return;
        var velocity = _rb.velocity;
        if (Math.Abs(velocity.y) <= verticalSupportThresholdAndSupport)
        {
            var isMovingUp = velocity.y > 0;
            _rb.velocity = new Vector2(velocity.x,
                isMovingUp ? verticalSupportThresholdAndSupport : -verticalSupportThresholdAndSupport);
        }
    }

    public void Die()
    {
        OnBallDeath?.Invoke(this);
        Destroy(gameObject, 1);
    }
}