using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class WinScreen : MonoBehaviour
{
    private Button _nextLevelButton;

    private void Start()
    {
        _nextLevelButton = GetComponent<Button>();
        
        _nextLevelButton.onClick.AddListener(() =>
        {
            GameManager.Instance.LoadNextLevel();
            GameManager.Instance.GameState = GameState.ReadyToPlay;
        });
    }
}
