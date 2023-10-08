using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [HideInInspector] public int bricksAlive = 0;

    private void Update()
    {
        if (bricksAlive <= 0)
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        int levelNumber = GameManager.Instance.currentLevel;
        if (levelNumber >= 2)
        {
            return;
        }
        levelNumber++;
        SceneManager.LoadScene("Level" + levelNumber);
    }
}
