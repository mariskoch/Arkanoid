using UnityEngine;
using Utils;

public class BrickManager : MonoBehaviour
{
    #region Singleton

    private static BrickManager _instance;

    public static BrickManager Instance => _instance;

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

    public Sprite[] Sprites;
    public GameObject LevelPassedCanvasPrefab;
    private GameObject _levelPassedInstance;
    
    [HideInInspector] public int bricksAlive = 0;

    private void Update()
    {
        if (bricksAlive <= 0  && GameManager.Instance.GameState == GameState.GameRunning)
        {
            GameManager.Instance.GameState = GameState.Win;
            BallManager.Instance.ResetBalls();
            if (_levelPassedInstance == null)
            {
                _levelPassedInstance = Instantiate(LevelPassedCanvasPrefab);
                UIManager.Instance.SaveScore();
                Timer.Instance.StopTimer();
            }
        }
    }
}
