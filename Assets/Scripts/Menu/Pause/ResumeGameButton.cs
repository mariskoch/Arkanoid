using Managers;
using UnityEngine;

namespace Menu.Pause
{
    public class ResumeGameButton : MonoBehaviour
    {
        public void ResumeGame()
        {
            PauseManager.ResumeGame();
        }
    }
}