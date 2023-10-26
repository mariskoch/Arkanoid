using TMPro;
using UnityEngine;

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

    private TextMeshProUGUI _timerText;
    private float _startTime;
    private bool _isRunning;

    private void Start()
    {
        _timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_isRunning)
        {
            UpdateTimer(Time.time - _startTime);
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
        _isRunning = true;
    }

    public void StopTimer(bool resetDisplay = false)
    {
        _isRunning = false;
        if (resetDisplay)
        {
            ResetTimer();
        }
    }

    public void ResetTimer()
    {
        _timerText.text = "00:00.000";
    }
}
