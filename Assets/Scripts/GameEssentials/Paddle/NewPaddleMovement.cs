using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace GameEssentials.Paddle
{
    public class NewPaddleMovement : MonoBehaviour
    {
        public static NewPaddleMovement Instance => _instance;
        
        [SerializeField] private float paddleSpeed = 6.5f;
        [SerializeField] private float deflectionStrengthInXDirection = 1.0f;

        private float _input;
        private Movement _movement;
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private BoxCollider2D _bc;
        private static NewPaddleMovement _instance;

        private void Awake()
        {
            _movement = new Movement();
            _rb = this.GetComponent<Rigidbody2D>();
            _sr = this.GetComponent<SpriteRenderer>();
            _bc = this.GetComponent<BoxCollider2D>();
            
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void OnEnable()
        {
            _movement.Player.Enable();
            _movement.Player.Movement.performed += InputActionPerformed;
            _movement.Player.Movement.canceled += InputActionCancelled;
        }

        private void OnDisable()
        {
            _movement.Player.Movement.performed -= InputActionPerformed;
            _movement.Player.Movement.canceled -= InputActionCancelled;
            _movement.Player.Disable();
        }

        private void FixedUpdate()
        {
            AdaptBoxColliderToSpriteSize();
            
            if (GameManager.Instance.GameState != GameState.GameRunning &&
                GameManager.Instance.GameState != GameState.ReadyToPlay)
            {
                _rb.velocity = Vector2.zero;
                return;
            }

            _rb.velocity = new Vector2(_input * paddleSpeed, 0);
        }

        private void AdaptBoxColliderToSpriteSize()
        {
            if (!_bc.size.x.Equals(_sr.size.x))
            {
                var size = _sr.size;
                _bc.size = new Vector2(size.x, size.y);
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Ball"))
            {
                var ballRb = col.gameObject.GetComponent<Rigidbody2D>();
                var hitPoint = col.contacts[0].point;
                var paddlePosition = this.gameObject.transform.position;
                var paddleCenter = new Vector2(paddlePosition.x, paddlePosition.y);
                ballRb.velocity =
                    new Vector2((hitPoint.x - paddleCenter.x) * deflectionStrengthInXDirection, 1).normalized *
                    BallManager.Instance.BallSpeed;
            }
        }

        private void InputActionPerformed(InputAction.CallbackContext ctx)
        {
            _input = _movement.Player.Movement.ReadValue<float>();
        }

        private void InputActionCancelled(InputAction.CallbackContext ctx)
        {
            _input = _movement.Player.Movement.ReadValue<float>();
        }
    }
}