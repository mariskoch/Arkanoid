using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Managers
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseScreenPrefab;
        
        private Movement _movement;
        private static GameObject _pauseScreenInstance;

        private void Awake()
        {
            _movement = new Movement();
        }

        private void OnEnable()
        {
            _movement.Player.PauseGame.Enable();
            _movement.Player.PauseGame.performed += HandlePause;
        }

        private void OnDisable()
        {
            _movement.Player.PauseGame.performed -= HandlePause;
            _movement.Player.PauseGame.Disable();
        }

        private void HandlePause(InputAction.CallbackContext ctx)
        {
            bool isPressed = ctx.ReadValueAsButton();
            if (!isPressed) return;

            if (GameManager.Instance.GameState == GameState.GameRunning) PauseGame();
            else if (GameManager.Instance.GameState == GameState.Paused) ResumeGame();
        }
        
        private void PauseGame()
        {
            GameManager.Instance.GameState = GameState.Paused;
            Time.timeScale = 0;
            _pauseScreenInstance = Instantiate(pauseScreenPrefab);
        }

        public static void ResumeGame()
        {
            if (GameManager.Instance.GameState != GameState.Paused) return;
            GameManager.Instance.GameState = GameState.GameRunning;
            Time.timeScale = 1;
            Destroy(_pauseScreenInstance);
        }
    }
}