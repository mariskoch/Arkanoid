using System;
using UnityEngine;
using Utils;

public class Ball : MonoBehaviour
{
    [SerializeField] private float verticalSupportThresholdAndSupport = 1.5f;
    [SerializeField] private float horizontalSupportKick = 0.75f;
    private Rigidbody2D _rb;
    private BoxCollider2D _bc;

    public static event Action<Ball> OnBallDeath;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bc = GetComponent<BoxCollider2D>();
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
            var isMovingUp = velocity.y > 0;
            _rb.velocity = new Vector2(velocity.x,
                isMovingUp ? verticalSupportThresholdAndSupport : -verticalSupportThresholdAndSupport);
        }

        // ensure, that the ball can NOT go perfectly vertical on the border
        var pos = this.transform.position;
        var bcSize = _bc.size;
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(pos.x + bcSize.x / 2 + 0.01f, pos.y), Vector2.right, bcSize.x / 4);
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(pos.x - bcSize.x / 2 - 0.01f, pos.y), Vector2.left, bcSize.x / 4);
        if (Mathf.Approximately(velocity.x, 0.0f) && hitRight.collider != null && hitRight.collider.CompareTag("Border"))
        {
            _rb.velocity = new Vector2(-horizontalSupportKick, velocity.y);
        } else if (Mathf.Approximately(velocity.x, 0.0f) && hitLeft.collider != null && hitLeft.collider.CompareTag("Border"))
        {
            _rb.velocity = new Vector2(+horizontalSupportKick, velocity.y);
        }
        
        // ensure, that the ball can not get vertically soft locked between unbreakable block and ceiling
        RaycastHit2D hitDown = Physics2D.Raycast(new Vector2(pos.x, pos.y - bcSize.y / 2 - 0.01f), Vector2.down);
        if (Mathf.Abs(velocity.x) < 1E-06f && hitDown.collider.CompareTag("UnbreakableBrick"))
        {
            if (velocity.x < 0.0f)
            {
                _rb.velocity = new Vector2(-horizontalSupportKick, velocity.y);
            }
            else
            {
                _rb.velocity = new Vector2(horizontalSupportKick, velocity.y);
            }
        }
    }

    public void Die()
    {
        OnBallDeath?.Invoke(this);
        Destroy(gameObject, 1);
    }
}