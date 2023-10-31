using System;
using GameOverScreen;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WinScreen
{
    public class WinScreenManager : MonoBehaviour
    {
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private TextMeshProUGUI levelPassedText;
        [SerializeField] private GameObject showScore;

        private void Start()
        {
            if (GameManager.Instance.currentLevel == GameManager.Instance.availableLevels)
            {
                nextLevelButton.gameObject.SetActive(false);
                levelPassedText.text = "Level passed! That was the last level.";
                GameObject obj = Instantiate(showScore, transform.position, Quaternion.identity);
                obj.transform.parent = transform.parent;
            }
        }
    }
}