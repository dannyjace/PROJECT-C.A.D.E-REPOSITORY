using System;
using UnityEngine;
using UnityEngine.InputSystem;
using RevolutionStudios.Input;
using static RevolutionStudios.Input.InputMaster;

namespace RevolutionStudios.GameManagement
{
    [CreateAssetMenu(fileName = "New Input Manager", menuName = "Revolution Studios/Data/Game Management/New Input Manager")]
    public class InputManager : ScriptableObject, IPlayerControlsActions
    {
        private InputMaster inputMaster;

        public event Action<Vector2> Move = delegate { };
        public event Action<Vector2> Look = delegate { };

        public event Action OnPlayerJumpPerformed = delegate { };
        public event Action OnPlayerStanceChangePerformed = delegate { };
        public event Action OnPlayerSprintPerformed = delegate { };
        public event Action OnPlayerAimPerformed = delegate { };
        public event Action OnPlayerAimCanceled = delegate { };
        public event Action OnPlayerFirePerformed = delegate { };
        public event Action OnPlayerFireCanceled = delegate { };
        public event Action OnPlayerPausePerformed = delegate { };

        public event Action OnPlayerInteractPerformed = delegate { };

        public Vector2 MoveInput => inputMaster.PlayerControls.Move.ReadValue<Vector2>();
        public Vector2 LookInput => inputMaster.PlayerControls.Look.ReadValue<Vector2>();


        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Move.Invoke(context.ReadValue<Vector2>());
            }
        }
        public void OnLook(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Look.Invoke(context.ReadValue<Vector2>());
            }
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnPlayerJumpPerformed.Invoke();
            }
        }
        public void OnStance(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnPlayerStanceChangePerformed.Invoke();
            }
        }
        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnPlayerSprintPerformed.Invoke();
            }
        }
        public void OnAim(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnPlayerAimPerformed.Invoke();
            }
            else if (context.canceled)
            {
                OnPlayerAimCanceled.Invoke();
            }
        }
        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnPlayerAimPerformed?.Invoke();
            }
            else if (context.canceled)
            {
                OnPlayerFireCanceled?.Invoke();
            }
        }
        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                OnPlayerPausePerformed?.Invoke();
            }
        }
        public void OnInteract(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                OnPlayerInteractPerformed?.Invoke();
            }
        }


        public void InitializeInputManager()
        {
            inputMaster = new InputMaster();

            inputMaster.Enable();

            inputMaster.PlayerControls.SetCallbacks(this);

            EnablePlayerControllerInput();
        }
        public void UninitializeInputManager()
        {
            inputMaster.PlayerControls.Disable();
            inputMaster.UserInterfaceControls.Disable();

            inputMaster.Disable();
        }

        public void EnablePlayerControllerInput()
        {
            inputMaster.UserInterfaceControls.Disable();
            inputMaster.PlayerControls.Enable();
        }
        public void EnablePlayerUserInterfaceInput()
        {
            inputMaster.PlayerControls.Disable();
            inputMaster.UserInterfaceControls.Enable();
        }

        
    }
}
