using RevolutionStudios.StateMachine;
using RevolutionStudios.Player.Utilities;
using UnityEngine;

namespace RevolutionStudios.Player.StateMachine
{
    public class PlayerActiveAimingState : State<PlayerController>
    {
        public override void OnEnterState()
        {
            
        }
        public override void OnUpdateState()
        {
            if (Context.AimingState == PlayerAimingState.Inactive)
            {
                //Context.AimingStateMachine.ChangeState<PlayerInactiveAimingState>();
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
