using TMPro;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    #region Singleton

    private static Multiplier _instance;

    public static Multiplier Instance => _instance;

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

    public float MultiplierValue { get; private set; }
    
    [SerializeField] private float startMultiplier = 10.0f;
    [SerializeField] private float declineRateInSeconds = 10.0f;
    // [SerializeField] private float lowestMultiplier = 0.8f;
    private TextMeshProUGUI _multiplierText;

    private void Start()
    {
        _multiplierText = GameObject.Find("Multiplier").GetComponent<TextMeshProUGUI>();
        ResetMultiplier();
    }

    private void Update()
    {
        if (Timer.Instance.IsRunning)
        {
            ComputeCurrentMultiplier();   
        }
    }

    private void ComputeCurrentMultiplier()
    {
        var timePassed = Timer.Instance.TimePassed;
        var currentMultiplier = startMultiplier - (timePassed / declineRateInSeconds);
        SetMultiplierDisplay(currentMultiplier);
        MultiplierValue = currentMultiplier;
    }

    private void SetMultiplierDisplay(float value)
    {
        var displayString = value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
        _multiplierText.text = $"{displayString}x";
    }
    
    public void ResetMultiplier()
    {
        MultiplierValue = startMultiplier;
        SetMultiplierDisplay(startMultiplier);
    }
}
