using UnityEngine;

namespace Gameplay
{
    public abstract class State
    {
        //"Context" needed for correct handling and execution of States.
        protected  Character Character;
        protected  StateMachine StateMachine;
        protected State(Character character, StateMachine stateMachine)
        {
            Character = character;
            this.StateMachine = stateMachine;
        }
        //Methods for States.
        public virtual void OnEnterState() { }
        public virtual void OnHandle() { }
        public virtual void OnExitState() { }
    }
}
