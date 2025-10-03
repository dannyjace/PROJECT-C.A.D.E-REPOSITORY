using RevolutionStudios.StateMachine;
using RevolutionStudios.Player.Utilities;

namespace RevolutionStudios.Player.StateMachine
{
    public class PlayerDefaultLocomotionState : State<PlayerController>
    {
        public override void OnEnterState()
        {
            Context.LocomotionState = PlayerLocomotionState.Default;
        }
        public override void OnUpdateState()
        {
            if (Context.LocomotionState == PlayerLocomotionState.Sprinting)
            {
                //Context.LocomotionStateMachine.ChangeState<PlayerSprintingLocomotionState>();
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
