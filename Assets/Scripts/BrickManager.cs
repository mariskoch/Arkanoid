using UnityEngine;

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
        if (bricksAlive <= 0  && GameManager.Instance.IsGameRunning)
        {
            GameManager.Instance.IsGameRunning = false;
            BallManager.Instance.ResetBalls();
            if (_levelPassedInstance == null)
            {
                _levelPassedInstance = Instantiate(LevelPassedCanvasPrefab);
            }
        }
    }
}
