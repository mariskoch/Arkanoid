using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Menu.Main
{
    public class TutorialButton : MonoBehaviour
    {
        public void StartTutorial()
        {
            // TODO: Evaluate if this is the best approach
            if (GameManager.Instance)
            {
                GameManager.Instance.GameState = GameState.ReadyToPlay;
            }
            PlayerPrefs.SetInt("StartTutorial", 1);
            SceneManager.LoadScene("Level1");
        }
    }
}