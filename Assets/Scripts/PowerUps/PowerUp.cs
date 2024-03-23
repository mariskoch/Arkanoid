using UnityEngine;

namespace PowerUps
{
    public abstract class PowerUp : MonoBehaviour
    {
        private Rigidbody2D _rb;

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.CompareTag("Paddle"))
            {
                ApplyPowerUp();
                Destroy(this.gameObject);
            }
            else if (coll.CompareTag("DeathBorder"))
            {
                Destroy(this.gameObject);
            }
        }

        private void Awake()
        {
            _rb = this.GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _rb.isKinematic = false;
            _rb.velocity = Vector2.down * 2;
        }

        protected abstract void ApplyPowerUp();
    }
}