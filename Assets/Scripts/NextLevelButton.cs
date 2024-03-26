using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class NextLevelButton : MonoBehaviour
{
    private Button _nextLevelButton;

    private void Start()
    {
        _nextLevelButton = GetComponent<Button>();
        
        _nextLevelButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ResetGameSpeedImmediately();
            GameManager.Instance.GameState = GameState.ReadyToPlay;
            GameManager.Instance.LoadNextLevel();
        });
    }
}
