using RevolutionStudios.StateMachine;
using RevolutionStudios.Player.Utilities;
using UnityEngine;

namespace RevolutionStudios.Player.StateMachine
{
    public class PlayerActiveAimingState : State<PlayerController>
    {
        public override void OnEnterState()
        {
            Context.AimingState = PlayerAimingState.Active;

            //Context.InputController.AimHeld += OnAim;

            Debug.Log(Context.AimingState);
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

        private void OnAim(bool active = false)
        {
            //Context.AnimationController.SetAimingWeight(0);
            //Context.AimingStateMachine.ChangeState<PlayerInactiveAimingState>();
        }
    }
}
