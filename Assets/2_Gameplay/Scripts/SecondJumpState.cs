namespace Gameplay
{
    public class SecondJumpState : State
    {
        public SecondJumpState(Character character, StateMachine stateMachine)
            : base(character, stateMachine) { }
        public override void OnEnterState()
        {
            _character.StartCoroutine(_character.Jump());
        }
        public override void OnHandle()
        {
            //In case something is wanted.
        }
        public override void OnExitState()
        {
            //In case something is wanted.
        }
    }
}