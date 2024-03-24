using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Managers
{
    public class PauseManager : MonoBehaviour
    {
        private Movement _movement;

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
        }

        private void ResumeGame()
        {
            GameManager.Instance.GameState = GameState.GameRunning;
            Time.timeScale = 1;
        }
    }
}