using RevolutionStudios.StateMachine;
using RevolutionStudios.Player.Utilities;

namespace RevolutionStudios.Player.StateMachine
{
    public class PlayerSprintingLocomotionState : State<PlayerController>
    {
        public override void OnEnterState()
        {
            Context.LocomotionState = PlayerLocomotionState.Sprinting;

            //Context.CameraController.UpdateCameraRigYOffset();
        }

        public override void OnUpdateState()
        {
            //Context.LocomotionState = Context.InputController.IsSprinting ? PlayerLocomotionState.Sprinting : PlayerLocomotionState.Default;

            switch (Context.LocomotionState)
            {
                case PlayerLocomotionState.Default:
                    //Context.LocomotionStateMachine.ChangeState<PlayerDefaultLocomotionState>();
                    break;

                case PlayerLocomotionState.Sprinting:
                    break;
            }
        }

        public override void OnLateUpdateState()
        {
            base.OnLateUpdateState();
        }

        public override void OnFixedUpdateState()
        {
            base.OnFixedUpdateState();
        }

        public override void OnExitState()
        {
            base.OnExitState();
        }
    }
}
