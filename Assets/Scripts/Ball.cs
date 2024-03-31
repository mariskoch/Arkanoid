using System;
using UnityEngine;
using Utils;

public class Ball : MonoBehaviour
{
    [SerializeField] private float verticalSupportThresholdAndSupport = 1.5f;
    [SerializeField] private float horizontalSupportKick = 1f;
    private Rigidbody2D _rb;
    private CircleCollider2D _cc;

    public static event Action<Ball> OnBallDeath;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cc = GetComponent<CircleCollider2D>();
        this.gameObject.layer = LayerMask.NameToLayer("NoCollisionBallLayer");
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("NoCollisionBallLayer"), LayerMask.NameToLayer("NoCollisionBallLayer"), true);
    }

    private void Update()
    {
        if (GameManager.Instance.GameState != GameState.GameRunning) return;
        var velocity = _rb.velocity;
        // ensure, that the ball can NOT go perfectly horizontal
        if (Math.Abs(velocity.y) <= verticalSupportThresholdAndSupport)
        {
            Debug.Log("Gave Ball a vertical kick");
            var isMovingUp = velocity.y > 0;
            _rb.velocity = new Vector2(velocity.x,
                isMovingUp ? verticalSupportThresholdAndSupport : -verticalSupportThresholdAndSupport);
        }

        // ensure, that the ball can NOT go perfectly vertical on the border
        var pos = this.transform.position;
        var ccRadius = _cc.radius;
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(pos.x + ccRadius + 0.01f, pos.y), Vector2.right, ccRadius / 2);
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(pos.x - ccRadius - 0.01f, pos.y), Vector2.left, ccRadius / 2);
        if (Mathf.Approximately(velocity.x, 0.0f) && hitRight.collider != null && hitRight.collider.CompareTag("Border"))
        {
            Debug.Log("Gave Ball a horizontal kick from wall");
            _rb.velocity = new Vector2(-horizontalSupportKick, velocity.y).normalized * BallManager.Instance.BallSpeed;
            return;
        } else if (Mathf.Approximately(velocity.x, 0.0f) && hitLeft.collider != null && hitLeft.collider.CompareTag("Border"))
        {
            Debug.Log("Gave Ball a horizontal kick from wall");
            _rb.velocity = new Vector2(+horizontalSupportKick, velocity.y).normalized * BallManager.Instance.BallSpeed;
            return;
        }
        
        // ensure, that the ball can not get vertically soft locked between unbreakable block and ceiling
        RaycastHit2D hitDown = Physics2D.Raycast(new Vector2(pos.x, pos.y - ccRadius - 0.01f), Vector2.down);
        if (Mathf.Abs(velocity.x) < horizontalSupportKick && hitDown.collider.CompareTag("UnbreakableBrick"))
        {
            if (velocity.x < 0.0f)
            {
                Debug.Log("Gave Ball a horizontal kick from softlock");
                _rb.velocity = new Vector2(-horizontalSupportKick, velocity.y).normalized * BallManager.Instance.BallSpeed;
            }
            else
            {
                Debug.Log("Gave Ball a horizontal kick from softlock");
                _rb.velocity = new Vector2(horizontalSupportKick, velocity.y).normalized * BallManager.Instance.BallSpeed;
            }
        }
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.GameState != GameState.GameRunning) return;
        if (!Mathf.Approximately(_rb.velocity.magnitude, BallManager.Instance.BallSpeed))
        {
            Debug.Log("Corrected Ballspeed");
            _rb.velocity = _rb.velocity.normalized * BallManager.Instance.BallSpeed;
        }
    }

    public void Die()
    {
        OnBallDeath?.Invoke(this);
        Destroy(gameObject, 1);
    }
}