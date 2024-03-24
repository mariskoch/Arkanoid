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
            var isPressed = ctx.ReadValueAsButton();
            if (!isPressed || GameManager.Instance.GameState != GameState.GameRunning) return;
            
            
        }
    }
}