using UnityEngine;

namespace Menu
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
        [SerializeField] private Canvas optionsCanvas;
        
        public void SwitchToOptions()
        {
            mainMenuCanvas.enabled = false;
            optionsCanvas.enabled = true;
        }

        public void SwitchToMainMenu()
        {
            mainMenuCanvas.enabled = true;
            optionsCanvas.enabled = false;
        }
    }
}