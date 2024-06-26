using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace B_Framework.DesignPatterns.State
{
    [Serializable]
    public abstract class AbstractStateMachine
    {
        public IState CurrentState { get; private set; }
        public event Action<IState> stateChanged;
        // set the starting state
        public void Initialize(IState state)
        {
            CurrentState = state;
            state.Enter();
            stateChanged?.Invoke(state);
        }
        // exit this state and enter another
        public void TransitionTo(IState nextState)
        {
            CurrentState.Exit();
            CurrentState = nextState;
            nextState.Enter();
            stateChanged?.Invoke(nextState);
        }
        // run in controller update
        public void LogicUpdate()
        {
            if (CurrentState != null)
            {
                CurrentState.LogicUpdate();
            }
        }
        // run in controller FixedUpdate
        public void PhysicUpdate()
        {
            if (CurrentState != null)
            {
                CurrentState.PhysicUpdate();
            }
        }
    }
}
