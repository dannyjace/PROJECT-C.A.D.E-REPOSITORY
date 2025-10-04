using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using RevolutionStudios.Input;
using static RevolutionStudios.Input.InputMaster;

namespace RevolutionStudios.Player.Data
{
    [CreateAssetMenu(fileName = "New Player Input Controller Data", menuName = "Revolution Studios/Data/Player/Input Controller Data")]
    public class PlayerInputControllerData : ScriptableObject, IPlayerControlsActions
    {
        private InputMaster inputMaster;

        public Vector2 MovementInput => inputMaster.PlayerControls.Move.ReadValue<Vector2>();
        public Vector2 LookInput => inputMaster.PlayerControls.Look.ReadValue<Vector2>();

        public float MoveX => inputMaster.PlayerControls.Move.ReadValue<Vector2>().x;
        public float MoveY => inputMaster.PlayerControls.Move.ReadValue<Vector2>().y;

        public float LookX => inputMaster.PlayerControls.Look.ReadValue<Vector2>().x;
        public float LookY => inputMaster.PlayerControls.Look.ReadValue<Vector2>().y;

        public bool isSprinting { get; private set; }
        public bool isAiming { get; private set; }



        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction<Vector2> Look = delegate { };

        public event UnityAction Jump = delegate { };
        public event UnityAction Stance = delegate { };
        public event UnityAction<bool> Sprint = delegate { };

        public event UnityAction<bool> Aim = delegate { };
        public event UnityAction<bool> Fire = delegate { };

        public event UnityAction Pause = delegate { };

        public event UnityAction Interact = delegate { };


        public void OnLook(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Look.Invoke(context.ReadValue<Vector2>());
            }
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Move.Invoke(context.ReadValue<Vector2>());
            }
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Jump.Invoke();
            }
        }
        public void OnStance(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Stance.Invoke();
                Sprint.Invoke(false);
                isSprinting = false;
            }
        }
        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed && MovementInput.magnitude > 0.1f && !isAiming)
            {
                Sprint.Invoke(true);
                isSprinting = !isSprinting;
            }
            else if (MovementInput.magnitude < 0.1f)
            {
                Sprint.Invoke(false);
                isSprinting = false;
            }
        }
        public void OnAim(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Sprint.Invoke(false);
                isSprinting = false;
                Aim.Invoke(true);
                isAiming = true;
            }
            else if (context.canceled)
            {
                Aim.Invoke(false);
                isAiming = false;
            }
        }
        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Fire.Invoke(true);
                Sprint.Invoke(false);
                isSprinting = false;
            }
            else if (context.canceled)
            {
                Fire.Invoke(false);
            }
        }
        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Pause.Invoke();
            }
        }
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Interact.Invoke();
            }
        }

        public void InitializeInputMaster()
        {
            inputMaster ??= new InputMaster();

            inputMaster.Enable();

            inputMaster.PlayerControls.SetCallbacks(this);

            inputMaster.PlayerControls.Enable();
        }

        
    }
}