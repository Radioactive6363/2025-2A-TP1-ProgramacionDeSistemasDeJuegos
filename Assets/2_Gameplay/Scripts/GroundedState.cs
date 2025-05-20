using UnityEngine;

namespace Gameplay
{
    public class GroundedState : State
    {
        public GroundedState(Character character, StateMachine stateMachine)
            : base(character, stateMachine) { }

        public override void OnEnterState()
        {
            //In case something is wanted.
        }

        public override void OnHandle()
        {
            _stateMachine.SetState(new FirstJumpState(_character, _stateMachine));
        }
        
        public override void OnExitState()
        {
            //In case something is wanted.
        }
    }
}