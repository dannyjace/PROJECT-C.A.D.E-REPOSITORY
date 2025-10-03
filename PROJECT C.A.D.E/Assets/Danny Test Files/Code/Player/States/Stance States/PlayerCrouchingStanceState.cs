using RevolutionStudios.Player.Utilities;
using RevolutionStudios.StateMachine;
using UnityEngine;

namespace RevolutionStudios.Player.StateMachine
{
    public class PlayerCrouchingStanceState : State<PlayerController>
    {
        public override void OnEnterState()
        {
            Context.StanceState = PlayerStanceState.Crouching;

            //Context.InputController.CrouchPressed += OnCrouch;
            //Context.InputController.ProneHeld += OnProne;

            //Context.CameraController.UpdateCameraRigYOffset();
            //Context.CameraController.UpdateCameraRigZOffset();
        }

        public override void OnUpdateState()
        {
            switch (Context.StanceState)
            {
                case PlayerStanceState.Standing:
                    //Context.StanceStateMachine.ChangeState<PlayerStandingStanceState>();
                    break;

                case PlayerStanceState.Prone:
                    //Context.StanceStateMachine.ChangeState<PlayerProneStanceState>();
                    break;
            }
        }

        public override void OnExitState()
        {
            //Context.InputController.CrouchPressed -= OnCrouch;
            //Context.InputController.ProneHeld -= OnProne;
        }

        //private void OnCrouch() => Context.AnimationController.HandleStanceInput(isProneInput: false);
        //private void OnProne() => Context.AnimationController.HandleStanceInput(isProneInput: true);
    }
}