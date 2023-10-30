using UnityEngine;
using Utils;
using Vector3 = UnityEngine.Vector3;

public class Paddle : MonoBehaviour
{
    #region Singleton

    private static Paddle _instance;

    public static Paddle Instance => _instance;

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

    public float paddleSpeed;
    public float deflectionStrength;
    private Rigidbody2D rb;
    private Vector3 movement = Vector3.zero;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        bc = this.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        AdaptBoxColliderToSpriteSize();

        if (GameManager.Instance.GameState != GameState.GameRunning &&
            GameManager.Instance.GameState != GameState.ReadyToPlay)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement = new Vector3(1 * paddleSpeed, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement = new Vector3(-1 * paddleSpeed, 0, 0);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) && movement.x > 0)
        {
            movement = Vector3.zero;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) && movement.x < 0)
        {
            movement = Vector3.zero;
        }

        rb.velocity = movement;
    }

    private void AdaptBoxColliderToSpriteSize()
    {
        if (!bc.size.x.Equals(sr.size.x))
        {
            Vector2 size = sr.size;
            bc.size = new Vector2(size.x, size.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            var ballRb = col.gameObject.GetComponent<Rigidbody2D>();
            var hitPoint = col.contacts[0].point;
            var paddlePosition = gameObject.transform.position;
            var paddleCenter = new Vector2(paddlePosition.x, paddlePosition.y);

            ballRb.velocity = Vector2.zero;
            ballRb.AddForce(new Vector2((hitPoint.x - paddleCenter.x) * deflectionStrength,
                BallManager.Instance.initialBallSpeed).normalized * BallManager.Instance.initialBallSpeed);
        }
    }
}