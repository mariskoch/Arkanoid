namespace PowerUps
{
    public class MultiBall : PowerUp
    {
        protected override void ApplyPowerUp()
        {
            foreach (var ball in BallManager.Instance.Balls)
            {
                BallManager.Instance.SpawnBalls(ball.gameObject.transform.position, 2);   
            }
        }
    }
}
