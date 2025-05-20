namespace Gameplay
{
    public class FirstJumpState : State
    {
        public FirstJumpState(Character character, StateMachine stateMachine)
            : base(character, stateMachine) { }

        public override void OnEnterState()
        {
            Character.StartCoroutine(Character.Jump());
        }

        public override void OnHandle()
        {
            StateMachine.SetState(new SecondJumpState(Character, StateMachine));
        }
        
        public override void OnExitState()
        {
            //In case something is wanted.
        }
    }
}