using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton

    private static UIManager _instance;

    public static UIManager Instance => _instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

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

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        livesText = GameObject.Find("Lives").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        
        GameManager.Instance.OnLiveReduction += SetLives;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLiveReduction -= SetLives;
    }

    public void SetLives(int lives)
    {
        var displayString = $"Lives:{Environment.NewLine}{lives}";
        livesText.text = displayString;
    }
}
