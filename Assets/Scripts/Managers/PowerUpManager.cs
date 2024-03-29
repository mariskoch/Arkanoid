using UnityEngine;
using Random = System.Random;

namespace Managers
{
    public class PowerUpManager : MonoBehaviour
    {
        [SerializeField] private float powerUpRate = 1.0f;
        [SerializeField] private GameObject[] powerUps;

        private AudioSource _ac;
        
        #region Singleton

        private static PowerUpManager _instance;
        public static PowerUpManager Instance => _instance;

        private void Awake()
        {
            _ac = this.GetComponent<AudioSource>();
            
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        #endregion
        
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

        public void PlaySound(AudioClip clip)
        {
            _ac.PlayOneShot(clip);
        }
    }
}
