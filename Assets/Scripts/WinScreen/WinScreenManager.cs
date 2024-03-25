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
                levelPassedText.fontSize = 36;
                var tr = transform.position;
                GameObject obj = Instantiate(showScore, new Vector3(tr.x, tr.y + 45, tr.z), Quaternion.identity);
                obj.transform.parent = transform.parent;
            }
        }
    }
}