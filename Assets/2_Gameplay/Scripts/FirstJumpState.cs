namespace Gameplay
{
    public class FirstJumpState : State
    {
        public FirstJumpState(Character character, StateMachine stateMachine)
            : base(character, stateMachine) { }

        public override void OnEnterState()
        {
            _character.StartCoroutine(_character.Jump());
        }

        public override void OnHandle()
        {
            _stateMachine.SetState(new SecondJumpState(_character, _stateMachine));
        }
        
        public override void OnExitState()
        {
            //In case something is wanted.
        }
    }
}