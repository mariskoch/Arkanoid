using System;
using Managers;
using UnityEngine;

namespace PowerUps
{
    public abstract class PowerUp : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        
        private Rigidbody2D _rb;

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.CompareTag("Paddle"))
            {
                ApplyPowerUp();
                if (audioClip != null) PowerUpManager.Instance.PlaySound(audioClip);
                Destroy(this.gameObject);
            }
            else if (coll.CompareTag("DeathBorder"))
            {
                Destroy(this.gameObject);
            }
        }

        private void OnEnable()
        {
            GameManager.OnGameOver += HandleGameOver;
            BrickManager.OnLevelPassed += HandleGameOver;
        }

        private void OnDisable()
        {
            GameManager.OnGameOver -= HandleGameOver;
            BrickManager.OnLevelPassed -= HandleGameOver;
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

        private void HandleGameOver()
        {
            _rb.velocity = Vector2.zero;
            _rb.isKinematic = true;
        }
    }
}
