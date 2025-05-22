namespace Gameplay
{
    public class JumpState : State
    {
        private PlayerController _playerController;
        public JumpState(PlayerController playerController)
        {
            _playerController = playerController; 
        }

        public override void OnEnterState()
        {
            _playerController?.Character.StartCoroutine(_playerController.Character.Jump());
            if (_playerController != null) 
                _playerController.IsAirborne = true;
        }

        public override void OnHandle()
        {
            _playerController.CurrentDirection *= _playerController.AirborneSpeedMultiplier;
            _playerController?.Character.SetDirection(_playerController.CurrentDirection);
        }
        
        public override void OnExitState()
        {
            //In case something is wanted.
        }
    }
}