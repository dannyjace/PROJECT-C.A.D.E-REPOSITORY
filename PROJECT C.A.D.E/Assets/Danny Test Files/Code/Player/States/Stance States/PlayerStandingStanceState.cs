using RevolutionStudios.StateMachine;
using RevolutionStudios.Player.Utilities;
using UnityEngine;

namespace RevolutionStudios.Player.StateMachine
{
    public class PlayerStandingStanceState : State<PlayerController>
    {
        public override void OnEnterState()
        {
            Context.StanceState = PlayerStanceState.Standing;

            //Context.InputController.CrouchPressed += OnCrouch;
            //Context.InputController.ProneHeld += OnProne;
        }

        public override void OnUpdateState()
        {
            switch (Context.StanceState)
            {
                case PlayerStanceState.Crouching:
                    //Context.StanceStateMachine.ChangeState<PlayerCrouchingStanceState>();
                    break;

                case PlayerStanceState.Prone:
                    //Context.StanceStateMachine.ChangeState<PlayerProneStanceState>();
                    break;
            }
        }

        public override void OnLateUpdateState()
        {
            
        }

        public override void OnFixedUpdateState()
        {
            
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
