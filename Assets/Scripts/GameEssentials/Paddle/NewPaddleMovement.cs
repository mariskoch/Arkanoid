using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace GameEssentials.Paddle
{
    public class NewPaddleMovement : MonoBehaviour
    {
        public static NewPaddleMovement Instance => _instance;
        public bool isTransforming { get; private set; } = false;
        public float paddleDefaultWidth { get; private set; }
        
        [SerializeField] private float paddleSpeed = 6.5f;
        [SerializeField] private float deflectionStrengthInXDirection = 1.0f;

        private float _paddleDefaultHeight;
        private float _input;
        private Movement _movement;
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private BoxCollider2D _bc;
        private static NewPaddleMovement _instance;
        private float _remainingTransformDuration = 0.0f;
        private bool _isUntransformed = true;
        private bool _isTransformationGrowth = false;
        private float _dynamicTransformationTargetWidth;

        private void Awake()
        {
            _movement = new Movement();
            _rb = this.GetComponent<Rigidbody2D>();
            _sr = this.GetComponent<SpriteRenderer>();
            _bc = this.GetComponent<BoxCollider2D>();

            var srSize = _sr.size;
            paddleDefaultWidth = srSize.x;
            _paddleDefaultHeight = srSize.y;
            _dynamicTransformationTargetWidth = paddleDefaultWidth;
            
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
            _movement.Player.Movement.Enable();
            _movement.Player.Movement.performed += InputActionHandler;
            _movement.Player.Movement.canceled += InputActionHandler;
        }

        private void OnDisable()
        {
            _movement.Player.Movement.performed -= InputActionHandler;
            _movement.Player.Movement.canceled -= InputActionHandler;
            _movement.Player.Movement.Disable();
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

        public void ChangePaddleSize(float targetWidth, float duration, float transformSpeed)
        {
            var currentWidth = _sr.size.x;
            if (Mathf.Approximately(currentWidth, targetWidth) && !_isUntransformed && !isTransforming)
            {
                Debug.Log("Case 1:");
                Debug.Log("Before: " + _remainingTransformDuration);
                _remainingTransformDuration += duration;
                Debug.Log("After: " + _remainingTransformDuration);
                return;
            } else if (!_isUntransformed && isTransforming && _isTransformationGrowth && targetWidth > paddleDefaultWidth)
            {
                Debug.Log("Case 2:");
                Debug.Log(_isTransformationGrowth);
                Debug.Log("Before: " + _remainingTransformDuration);
                _remainingTransformDuration += duration;
                Debug.Log("After: " + _remainingTransformDuration);
                return;
            } else if (!_isUntransformed && isTransforming && !_isTransformationGrowth && targetWidth < paddleDefaultWidth)
            {
                Debug.Log("Case 3:");
                Debug.Log("Before: " + _remainingTransformDuration);
                _remainingTransformDuration += duration;
                Debug.Log("After: " + _remainingTransformDuration);
                return;
            } else if (!_isUntransformed && !isTransforming && currentWidth > paddleDefaultWidth && targetWidth < paddleDefaultWidth)
            {
                _dynamicTransformationTargetWidth = targetWidth;
                StartCoroutine(AnimatePaddleToWidth(transformSpeed));
                _remainingTransformDuration = duration;
                return;
            } else if (!_isUntransformed && !isTransforming && currentWidth < paddleDefaultWidth && targetWidth > paddleDefaultWidth)
            {
                _dynamicTransformationTargetWidth = targetWidth;
                StartCoroutine(AnimatePaddleToWidth(transformSpeed));
                _remainingTransformDuration = duration;
                return;
            } else if (!_isUntransformed && isTransforming && _isTransformationGrowth && targetWidth < paddleDefaultWidth)
            {
                Debug.Log("Schrumpf Call");
                _dynamicTransformationTargetWidth = targetWidth;
                _remainingTransformDuration = duration;
                return;
            } else if (!_isUntransformed && isTransforming && !_isTransformationGrowth && targetWidth > paddleDefaultWidth)
            {
                Debug.Log("Grow Call");
                _dynamicTransformationTargetWidth = targetWidth;
                _remainingTransformDuration = duration;
                return;
            }
            _dynamicTransformationTargetWidth = targetWidth;
            StartCoroutine(AnimatePaddleToWidth(transformSpeed));
            StartCoroutine(ResetPaddleAfterTime(duration, transformSpeed));
        }

        private IEnumerator AnimatePaddleToWidth(float transformSpeed)
        {
            isTransforming = true;
            if (!Mathf.Approximately(_dynamicTransformationTargetWidth, paddleDefaultWidth)) _isUntransformed = false;
            
            
            var currentWidth = this._sr.size.x;
            
            while (!Mathf.Approximately(currentWidth, _dynamicTransformationTargetWidth))
            {
                if (_dynamicTransformationTargetWidth > paddleDefaultWidth) _isTransformationGrowth = true;
                else if (_dynamicTransformationTargetWidth < paddleDefaultWidth) _isTransformationGrowth = false;
                currentWidth = Mathf.MoveTowards(currentWidth, _dynamicTransformationTargetWidth, Time.deltaTime * transformSpeed);
                _sr.size = new Vector2(currentWidth, _paddleDefaultHeight);
                yield return null;
            }
            isTransforming = false;
            _isTransformationGrowth = false;
            if (Mathf.Approximately(currentWidth, paddleDefaultWidth))
            {
                _remainingTransformDuration = 0.0f;
                _isUntransformed = true;
            }
        }

        private IEnumerator ResetPaddleAfterTime(float duration, float transformSpeed)
        {
            // yield return new WaitForSeconds(duration);
            
            _remainingTransformDuration = duration;
            while (_remainingTransformDuration > 0.0f)
            {
                _remainingTransformDuration -= Time.deltaTime;
                yield return null;
            }
            
            _dynamicTransformationTargetWidth = paddleDefaultWidth;
            StartCoroutine(AnimatePaddleToWidth(transformSpeed));
        }

        private void InputActionHandler(InputAction.CallbackContext ctx)
        {
            _input = ctx.ReadValue<float>();
        }
    }
}