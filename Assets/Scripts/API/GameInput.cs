using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace API
{
    public class GameInput : IGameInput
    {
        public event Action OnClick;

        public Vector2 MousePosition { get; private set; }
    
        private static readonly PlayerInputAction PlayerInputAction;

        static GameInput()
        {
            PlayerInputAction = new PlayerInputAction();
            PlayerInputAction.Interact.Enable();
        }

        public GameInput()
        {
            PlayerInputAction.Interact.SetCallbacks(this);
        }

        public void DisableInput()
        {
            
        }

        public void OnTouchInput(InputAction.CallbackContext context)
        {
            Debug.Log(context.ToString());
            if (context.started)
            {
                MousePosition = PlayerInputAction.Interact.TouchPosition.ReadValue<Vector2>();
                OnClick?.Invoke();
            }
        }

        public void OnTouchPosition(InputAction.CallbackContext context)
        {
        
        }
    }
}