using RevolutionStudios.StateMachine;
using RevolutionStudios.Player.Utilities;

namespace RevolutionStudios.Player.StateMachine
{
    public class PlayerSprintingLocomotionState : State<PlayerController>
    {
        public override void OnEnterState()
        {
            Context.LocomotionState = PlayerLocomotionState.Sprinting;
        }

        public override void OnUpdateState()
        {
            if (GameManager.instance.InputManager.MoveInput.y < 0.5f)
            {
                Context.LocomotionState = PlayerLocomotionState.Default;
            }

            if (Context.LocomotionState == PlayerLocomotionState.Default)
            {
                //Context.LocomotionStateMachine.ChangeState<PlayerDefaultLocomotionState>();
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
