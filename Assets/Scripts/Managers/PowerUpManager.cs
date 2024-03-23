using UnityEngine;
using Random = System.Random;

namespace Managers
{
    public class PowerUpManager : MonoBehaviour
    {
        [SerializeField] private float powerUpRate = 1.0f;
        [SerializeField] private GameObject[] powerUps; 
        
        private void OnEnable()
        {
            Brick.OnBrickDestruction += HandleBrickDestruction;
        }

        private void OnDisable()
        {
            Brick.OnBrickDestruction -= HandleBrickDestruction;
        }

        private void HandleBrickDestruction(Brick brick)
        {
            var random = new Random();
            if ((float) random.NextDouble() > powerUpRate) return;
            
            var powerUpIndex = Mathf.FloorToInt((float) random.NextDouble() * powerUps.Length);
            Instantiate(powerUps[powerUpIndex], brick.gameObject.transform.position, Quaternion.identity);
        }
    }
}
