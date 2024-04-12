using TMPro;
using UnityEngine;

namespace TimerUtils
{
    public class Timer : MonoBehaviour
    {
        #region Singleton

        private static Timer _instance;

        public static Timer Instance => _instance;

        private void Awake()
        {
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

        public float TimePassed { get; private set; }
        public bool IsRunning { get; private set; }

        private TextMeshProUGUI _timerText;
        private float _startTime;

        private void Start()
        {
            _timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
            TimePassed = 0;
            IsRunning = false;
        }

        private void Update()
        {
            if (IsRunning)
            {
                var elapsedTime = Time.time - _startTime;
                TimePassed = elapsedTime;
                UpdateTimer(elapsedTime);
            }
        }

        private void UpdateTimer(float elapsedTime)
        {
            var minutes = ((int)(elapsedTime / 60)).ToString("D2");
            var seconds = ((int)(elapsedTime % 60)).ToString("D2");
            var milliSeconds = ((int)((elapsedTime - (int)elapsedTime) * 1000)).ToString("D3");
            _timerText.text = $"{minutes}:{seconds}.{milliSeconds}";
        }

        public void StartTimer()
        {
            _startTime = Time.time;
            IsRunning = true;
        }

        public void StopTimer(bool resetDisplay = false)
        {
            IsRunning = false;
            if (resetDisplay)
            {
                ResetTimer();
            }
        }

        public void ResetTimer()
        {
            _timerText.text = "00:00.000";
            TimePassed = 0;
            Multiplier.Instance.ResetMultiplier();
        }
    }
}