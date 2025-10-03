using System;
using System.Collections.Generic;

namespace RevolutionStudios.StateMachine
{
    public class StateMachine<TContext>
    {
        private readonly TContext _context;
        private readonly Dictionary<Type, IState> _states = new();
        private IState _currentState;

        public StateMachine(TContext context)
        {
            _context = context;
        }
        public void AddState<TState>(TState state) where TState : IState
        {
            if (state is State<TContext> typedState)
            {
                typedState.SetContext(_context);
            }

            _states[typeof(TState)] = state;
        }
        public void ChangeState<TState>() where TState : IState
        {
            if (_currentState != null) { return; }

            _currentState?.OnExitState();
            _currentState = _states[typeof(TState)];
            _currentState.OnEnterState();
        }

        public void Update() => _currentState?.OnUpdateState();
        public void LateUpdate() => _currentState?.OnLateUpdateState();
        public void FixedUpdate() => _currentState?.OnFixedUpdateState();

        public Type GetCurrentStateType() => _currentState?.GetType();
    }
}
