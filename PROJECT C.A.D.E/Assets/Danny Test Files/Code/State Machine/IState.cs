namespace RevolutionStudios.StateMachine
{
    public interface IState
    {
        void OnEnterState();
        void OnUpdateState();
        void OnLateUpdateState();
        void OnFixedUpdateState();
        void OnExitState();
    }
}
