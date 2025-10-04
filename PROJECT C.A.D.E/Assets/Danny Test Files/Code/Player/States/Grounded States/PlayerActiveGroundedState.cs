using UnityEngine;
using RevolutionStudios.Player.Utilities;
using RevolutionStudios.StateMachine;

namespace RevolutionStudios.Player.StateMachine
{
    public class PlayerActiveGroundedState : State<PlayerController>
    {
        public override void OnEnterState()
        {
            
        }
        public override void OnUpdateState()
        {
            if (Context.GroundedState == PlayerGroundedState.Airborne)
            {
                //Context.GroundedStateMachine.ChangeState<PlayerInactiveGroundedState>();
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