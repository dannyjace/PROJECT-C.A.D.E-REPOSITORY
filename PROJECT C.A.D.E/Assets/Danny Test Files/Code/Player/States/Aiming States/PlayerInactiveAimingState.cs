using RevolutionStudios.StateMachine;
using RevolutionStudios.Player.Utilities;

namespace RevolutionStudios.Player.StateMachine
{
    public class PlayerInactiveAimingState : State<PlayerController>
    {
        public override void OnEnterState()
        {

        }
        public override void OnUpdateState()
        {
            if (Context.AimingState == PlayerAimingState.Active)
            {
                //Context.AimingStateMachine.ChangeState<PlayerActiveAimingState>();
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
            
        }
    }
}
