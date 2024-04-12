using GameEssentials.Ball;
using Managers;
using UnityEngine;

namespace GameOverScreen
{
    public class DeathScreen : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.CompareTag("Ball"))
            {
                Ball ball = coll.GetComponent<Ball>();
                BallManager.Instance.Balls.Remove(ball);
                ball.Die();
            }
        }
    }
}