using System;
using GameOverScreen;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            if (SceneManager.GetActiveScene().name.Contains("Tutorial"))
            {
                if (GameManager.Instance.currentTutorial == GameManager.Instance.availableTutorials)
                {
                    nextLevelButton.gameObject.SetActive(false);
                    levelPassedText.text = "That was the Tutorial. You are now ready to play!";
                    levelPassedText.fontSize = 36;
                    // var tr = transform.position;
                    // GameObject obj = Instantiate(showScore, new Vector3(tr.x, tr.y + 45, tr.z), Quaternion.identity);
                    // obj.transform.parent = transform.parent;
                }
            }
            else
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
}