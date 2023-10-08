using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance => _instance;

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

    public bool isGameRunning { get; set; }
    [HideInInspector] public int currentLevel = 1;

    private void Start()
    {
        // Screen.SetResolution(450, 437, false);
    }
}