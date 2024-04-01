using System.Collections;
using Managers;
using UnityEngine;

namespace PowerUps
{
    public class SlowMotion : PowerUp
    {
        [SerializeField] private float gameSpeed = 0.5f;
        [SerializeField] private float duration = 5.0f;
        
        protected override void ApplyPowerUp()
        {
            GameManager.Instance.ChangeGameSpeedForDuration(gameSpeed, duration * gameSpeed);   
        }
    }
}