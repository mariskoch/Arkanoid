using UnityEngine;
using Utils;

namespace GameEssentials.Brick
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float degreesPerSecondRotation = 45.0f;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = this.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.GameState != GameState.GameRunning)
            {
                _rb.angularVelocity = 0;
                return;
            }
            _rb.angularVelocity = degreesPerSecondRotation;
        }
    }
}