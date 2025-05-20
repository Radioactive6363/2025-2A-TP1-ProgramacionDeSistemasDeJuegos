using UnityEngine;

namespace Gameplay
{
    public class StateMachine
    {
        //Requires Character owner to work. So its available to all classes
        //that implements Character.
        private Character _character;
        private State _currentState;
        private Coroutine _currentJumpCoroutine;
        //Lambda expression for Bool.
        public bool IsAirborne => _currentState != State.Grounded;
        //Constructor
        public StateMachine(Character character)
        {
            _character = character;
            _currentState = State.Grounded;
        }
        //Jumping Logic
        public void TryJump()
        {
            if (_currentState == State.SecondJump)
            {
                return;
            }
            //Lambda Expression to get JumpState.
            _currentState = _currentState == State.Grounded 
                ? State.FirstJump 
                : State.SecondJump;

            if (_currentJumpCoroutine != null)
            {
                _character.StopCoroutine(_currentJumpCoroutine);
            }
            _currentJumpCoroutine = _character.StartCoroutine(_character.Jump());
        }
        //Resets state when is on the ground.
        public void ResetJumpState()
        {
            _currentState = State.Grounded;
        }
    }
}
