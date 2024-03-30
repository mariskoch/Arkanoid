using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu.Main
{
    public class TutorialButton : MonoBehaviour
    {
        public void StartTutorial()
        {
            SceneManager.LoadScene("Tutorial1");
        }
    }
}