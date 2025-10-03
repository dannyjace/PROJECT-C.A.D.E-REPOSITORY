using RevolutionStudios.StateMachine;
using RevolutionStudios.Player.Utilities;

namespace RevolutionStudios.Player.StateMachine
{
    public class PlayerInactiveAimingState : State<PlayerController>
    {
        public override void OnEnterState()
        {
            Context.AimingState = PlayerAimingState.Inactive;

            //Context.InputController.AimHeld += OnAim;
        }
        public override void OnUpdateState()
        {
            
        }
        public override void OnLateUpdateState()
        {
            
        }
        public override void OnFixedUpdateState()
        {
            
        }
        public override void OnExitState()
        {
            //Context.InputController.AimHeld -= OnAim;
        }

        private void OnAim(bool active = true)
        {
            //Context.AnimationController.SetTargetLookIKDampTime(0.25f);
            //Context.AnimationController.SetAimingWeight(1);
            //Context.AimingStateMachine.ChangeState<PlayerActiveAimingState>();
        }
    }
}
