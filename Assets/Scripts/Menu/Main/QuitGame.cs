using UnityEngine;
using UnityEngine.UI;

namespace Menu.Main
{
    public class QuitGame : MonoBehaviour
    {
        private Button _quitButton;

        private void Start()
        {
            _quitButton = GetComponent<Button>();
            _quitButton.onClick.AddListener(() => { Application.Quit(); });
        }
    }
}