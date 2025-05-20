using UnityEngine;

namespace Gameplay
{
    public class StateMachine
    {
        private State _currentState;
        //Lamda Expression to check currentState of Character.
        public bool IsAirborne => _currentState is not GroundedState;
        //State Machine Constructor.
        public StateMachine(Character character)
        {
            SetState(new GroundedState(character, this));
        }
        //Method for changing states, via exiting states and entering new states.
        public void SetState(State newState)
        {
            _currentState?.OnExitState();
            _currentState = newState;
            Debug.Log(_currentState); //Debugging purposes.
            _currentState.OnEnterState();
        }
        //Handler for States in case of execution. In this case, via input in PlayerController.
        public void TryHandleInput()
        {
            _currentState?.OnHandle();
        }
    }
}