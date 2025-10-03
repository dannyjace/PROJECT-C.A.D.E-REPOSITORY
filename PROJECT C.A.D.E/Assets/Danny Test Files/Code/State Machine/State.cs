namespace RevolutionStudios.StateMachine
{
    public abstract class State<TContext> : IState
    {
        protected TContext Context { get; private set; }

        public void SetContext(TContext context) => Context = context;

        public virtual void OnEnterState() { }
        public virtual void OnUpdateState() { }
        public virtual void OnLateUpdateState() { }
        public virtual void OnFixedUpdateState() { }
        public virtual void OnExitState() { }
    }
}