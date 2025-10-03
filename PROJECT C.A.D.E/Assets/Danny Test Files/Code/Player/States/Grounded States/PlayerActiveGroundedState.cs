using RevolutionStudios.Player.Utilities;
using RevolutionStudios.StateMachine;

namespace RevolutionStudios.Player.StateMachine
{
    public class PlayerActiveGroundedState : State<PlayerController>
    {
        public override void OnEnterState()
        {
            Context.GroundedState = PlayerGroundedState.Grounded;
        }
        public override void OnUpdateState()
        {
            Context.GroundedState = Context.MovementController.GetGroundedState();

            if (Context.GroundedState == PlayerGroundedState.Airborne)
            {
                Context.GroundedStateMachine.ChangeState<PlayerInactiveGroundedState>();
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