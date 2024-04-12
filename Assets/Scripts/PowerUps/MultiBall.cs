using System.Collections.Generic;
using GameEssentials.Ball;
using Managers;

namespace PowerUps
{
    public class MultiBall : PowerUp
    {
        protected override void ApplyPowerUp()
        {
            var ballsCopy = new List<Ball>(BallManager.Instance.Balls);
            foreach (var ball in ballsCopy)
            {
                BallManager.Instance.SpawnBalls(ball.gameObject.transform.position, 2);   
            }
        }
    }
}
