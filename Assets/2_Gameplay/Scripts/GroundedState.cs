namespace Gameplay
{
    public class GroundedState : State
    {
        //Not yet implemented since there is no real need for this, unless in the future something is wanted.
        //Works as a default state for the State Machine on initializing for the Player.
        protected PlayerController PlayerController;
        public GroundedState(PlayerController playerController)
        {
            PlayerController = playerController; 
        }
        
        public override void OnEnterState()
        {
            //In case something is wanted.
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