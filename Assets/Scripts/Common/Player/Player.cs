using Common.Ships;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Player
{
    public class Player
    {
        private readonly Ship _controllableShip;

        public Ship ControllableShip => _controllableShip;

        public Player(Ship controllableShip)
        {
            _controllableShip = controllableShip;
        }

        public void SetPosition(Vector2 position) => _controllableShip.SetPosition(position);
        
        public void StartMoving(InputAction.CallbackContext callbackContext)
        {
            Vector2 direction = callbackContext.ReadValue<Vector2>();
            
            _controllableShip.StartMoving(direction);
        }

        public void StopMoving(InputAction.CallbackContext callbackContext) => _controllableShip.StopMoving();
    }
}