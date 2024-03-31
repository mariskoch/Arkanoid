using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            if (SceneManager.GetActiveScene().name.Contains("Tutorial"))
            {
                GameManager.Instance.LoadNextTutorial();
            }
            else
            {
                GameManager.Instance.LoadNextLevel();   
            }
        });
    }
}
