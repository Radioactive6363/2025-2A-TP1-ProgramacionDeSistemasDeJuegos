using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class StateMachine
    {
        private State _currentState;
        //Dictionary for Transitions between States.
        private readonly Dictionary<(State,string), State> _stateTransitions = new();
        //State Machine Constructor + Initializer.
        public StateMachine(State initialState)
        {
            ChangeState(initialState);
        }
        public void ChangeState(State state)
        {
            _currentState = state;
            _currentState?.OnEnterState();
        }
        //Addition of Transitions via an Initial State and a string "TagTransition", returning the state to transition.
        public void AddTransition(State fromState, string tagTransition, State nextState)
        {
            _stateTransitions.Add((fromState, tagTransition), nextState);
        }
        //Method for transitioning states, via exiting states and entering new states using a transitionTag.
        public void TryTransition(string transitionTag)
        {
            var previousState = _currentState;
            if (_stateTransitions.TryGetValue((_currentState, transitionTag), out var nextState) && nextState != previousState)
            {
                previousState?.OnExitState();
                _currentState = nextState;
                _currentState?.OnEnterState();
            }
            Debug.Log(_currentState); //Debugging purposes.
        }
        //Handler for States in case of execution. In this case, via input in PlayerController.
        public void TryHandleInput()
        {
            _currentState?.OnHandle();
        }
    }
}