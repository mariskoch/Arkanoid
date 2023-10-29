using UnityEngine;
using UnityEngine.InputSystem;

namespace GameEssentials.Pedal
{
    public class NewPlayerMovement : MonoBehaviour
    {
        private Movement _movement;

        private void Awake()
        {
            _movement = new Movement();
        }

        private void OnEnable()
        {
            _movement.Player.Enable();
            _movement.Player.Movement.performed += OnPlayerMovementPerformed;
            _movement.Player.Movement.canceled += OnPlayerMovementCancelled;
        }

        private void OnDisable()
        {
            _movement.Player.Disable();
            _movement.Player.Movement.performed -= OnPlayerMovementPerformed;
            _movement.Player.Movement.canceled -= OnPlayerMovementCancelled;
        }

        private void OnPlayerMovementPerformed(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<float>();
            
            // TODO: Work further with input from keys to change pedal movement
            Debug.Log("Input: " + input);
        }

        private void OnPlayerMovementCancelled(InputAction.CallbackContext context)
        {
            
        }
    }
}