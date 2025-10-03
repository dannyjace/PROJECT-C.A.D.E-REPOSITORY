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
        #region Input Controller Fields

        private InputMaster inputMaster;

        public Vector2 MovementInput => inputMaster.PlayerControls.Move.ReadValue<Vector2>();
        public Vector2 LookInput => inputMaster.PlayerControls.Look.ReadValue<Vector2>();

        public float MoveX => inputMaster.PlayerControls.Move.ReadValue<Vector2>().x;
        public float MoveY => inputMaster.PlayerControls.Move.ReadValue<Vector2>().y;

        public float LookX => inputMaster.PlayerControls.Look.ReadValue<Vector2>().x;
        public float LookY => inputMaster.PlayerControls.Look.ReadValue<Vector2>().y;

        #endregion

        #region Input Controller Events

        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction<Vector2> Look = delegate { };

        public event UnityAction Jump = delegate { };
        public event UnityAction Stance = delegate { };
        public event UnityAction<bool> Sprint = delegate { };

        public event UnityAction<bool> Aim = delegate { };
        public event UnityAction<bool> Fire = delegate { };

        public event UnityAction Pause = delegate { };

        #endregion

        #region Input Action Events

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
                Debug.Log("Jump Performed");
            }
        }
        public void OnStance(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Stance.Invoke();
            }
        }
        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed && MovementInput.normalized.magnitude > 0.1f)
            {
                Sprint.Invoke(true);
                Debug.Log("Sprint Performed");
            }
            else
            {
                Sprint.Invoke(false);
                Debug.Log("Sprint Canceled");
            }
        }
        public void OnAim(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Aim.Invoke(true);
                Debug.Log("Aim Performed");
            }
            else if (context.canceled)
            {
                Aim.Invoke(false);
                Debug.Log("Aim Canceled");
            }
        }
        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Fire.Invoke(true);
                Debug.Log("Fire Performed");
            }
            else if (context.canceled)
            {
                Fire.Invoke(false);
                Debug.Log("Fire Canceled");
            }
        }
        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Pause.Invoke();
            }
        }

        #endregion

        #region Input Controller Methods

        public void InitializeInputMaster()
        {
            inputMaster ??= new InputMaster();

            inputMaster.Enable();

            inputMaster.PlayerControls.SetCallbacks(this);

            inputMaster.PlayerControls.Enable();
        }

        #endregion
    }
}