using UnityEngine;

namespace Menu.Main
{
    public class MenuManager : MonoBehaviour
    {
        #region Singleton

        private static MenuManager _instance;

        public static MenuManager Instance => _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        #endregion
        
        [SerializeField] private Canvas mainMenuCanvas;
        
        public void SwitchToOptions()
        {
            mainMenuCanvas.enabled = false;
        }

        public void SwitchToMainMenu()
        {
            mainMenuCanvas.enabled = true;
        }
    }
}