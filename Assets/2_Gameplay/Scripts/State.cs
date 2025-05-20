using UnityEngine;

namespace Gameplay
{
    public abstract class State
    {
        //"Context" needed for correct handling and execution of States.
        protected readonly Character _character;
        protected readonly StateMachine _stateMachine;
        protected State(Character character, StateMachine stateMachine)
        {
            _character = character;
            _stateMachine = stateMachine;
        }
        //Methods for States.
        public virtual void OnEnterState() { }
        public virtual void OnHandle() { }
        public virtual void OnExitState() { }
    }
}
