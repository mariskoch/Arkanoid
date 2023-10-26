using System;
using UnityEngine;
using UnityEngine.UIElements;
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
            Rigidbody2D ballRb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = col.contacts[0].point;
            Vector3 paddlePosition = this.gameObject.transform.position;
            Vector3 paddleCenter = new Vector3(paddlePosition.x, paddlePosition.y, 0);

            ballRb.velocity = Vector2.zero;
            ballRb.AddForce(new Vector2((hitPoint.x - paddleCenter.x) * deflectionStrength,
                BallManager.Instance.initialBallSpeed).normalized * BallManager.Instance.initialBallSpeed);
        }
    }
}
