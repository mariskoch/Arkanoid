using GameEssentials.Paddle;
using UnityEngine;

namespace PowerUps
{
    public class PaddleTransformer : PowerUp
    {
        [SerializeField] private float sizeMultiplier = 1.0f;
        [SerializeField] private float transformMaintainDurationInSeconds = 5.0f;
        [SerializeField] private float transformSpeed = 10.0f;
        
        protected override void ApplyPowerUp()
        {
            if (NewPaddleMovement.Instance == null) return;
            
            var currentSize = NewPaddleMovement.Instance.paddleDefaultWidth;
            NewPaddleMovement.Instance.ChangePaddleSize(currentSize * sizeMultiplier, transformMaintainDurationInSeconds, transformSpeed);
        }
    }
}