using UnityEngine;

namespace Gameplay
{
    public abstract class State
    {
        //"Context" needed for correct handling and execution of States.
        // protected State(Character character)
        // {
        //     Character = character;
        // }
        //Methods for States.
        public virtual void OnEnterState() { }
        public virtual void OnHandle() { }
        public virtual void OnExitState() { }
    }
}
