using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Menu.Main
{
    public class TutorialButton : MonoBehaviour
    {
        public void StartTutorial()
        {
            if (GameManager.Instance)
            {
                GameManager.Instance.GameState = GameState.ReadyToPlay;
            }
            PlayerPrefs.SetInt("StartTutorial", 1);
            SceneManager.LoadScene("Level1");
        }
    }
}