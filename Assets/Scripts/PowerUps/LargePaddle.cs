using GameEssentials.Paddle;
using UnityEngine;

namespace PowerUps
{
    public class LargePaddle : PowerUp
    {
        [SerializeField] private float paddleGrowSize = 20.0f;
        [SerializeField] private float transformMaintainDuration = 5.0f;
        [SerializeField] private float transformSpeed = 10.0f;
        
        protected override void ApplyPowerUp()
        {
            if (NewPaddleMovement.Instance != null && !NewPaddleMovement.Instance.isTransforming)
            {
                NewPaddleMovement.Instance.ChangePaddleSize(paddleGrowSize, transformMaintainDuration, transformSpeed);
            }
        }
    }
}