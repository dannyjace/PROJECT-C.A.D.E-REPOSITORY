using RevolutionStudios.Player.Utilities;
using RevolutionStudios.StateMachine;
using UnityEngine;

namespace RevolutionStudios.Player.StateMachine
{
    public class PlayerProneStanceState : State<PlayerController>
    {
        public override void OnEnterState()
        {
            Context.StanceState = PlayerStanceState.Prone;

            //Context.InputController.CrouchPressed += OnCrouch;
            //Context.InputController.ProneHeld += OnProne;

            //Context.CameraController.UpdateCameraRigYOffset();
            //Context.CameraController.UpdateCameraRigZOffset();
        }

        public override void OnUpdateState()
        {
            switch (Context.StanceState)
            {
                case PlayerStanceState.Crouching:
                    //Context.StanceStateMachine.ChangeState<PlayerCrouchingStanceState>();
                    break;

                case PlayerStanceState.Standing:
                    //Context.StanceStateMachine.ChangeState<PlayerStandingStanceState>();
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