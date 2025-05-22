using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    [RequireComponent(typeof(Character))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveInput;
        [SerializeField] private InputActionReference jumpInput;
        [SerializeField] private float airborneSpeedMultiplier = .5f;
        /*NOTE
            A State Machine is created with "CurrentState" handling using the AddTransition, TryTransition 
            and TryHandleInput methods. The transitions are controlled via an EntryState and an ExitState, and a string
            tag, all stored in a dictionary.
             Each `State` class can override `OnEnterState`, `OnHandle`, and `OnExitState` to define 
            specific behaviors per state and add different variables inside them if needed. 
            I would consider this a Strategy Pattern, but because a "Context" is required within the States,
            In this case, Character and StateMachine, it cannot be considered a Strategy Pattern.
            Some States do not have OnEnterState, OnHandle or OnEndState implemented, but they are still part of the 
            State Machine and can be used in the future to implement other features (animations, sounds, etc.).
            This state machine is designed to be reusable for any component that implements 
            the `Character` class.
        */
        private Vector3 _currentDirection;
        private StateMachine _stateMachine;
        private State _groundedState;
        private State _jumpState;
        private State _doubleJumpState;
        
        public Character Character { get; private set; }

        public Vector3 CurrentDirection { get; set; }
        public float AirborneSpeedMultiplier => airborneSpeedMultiplier;
        public bool IsAirborne { get; set; }
        
        //Creation of NewStateMachine for the Character.
        private void Awake()
        {
            Character = GetComponent<Character>();
            _groundedState = new GroundedState(this);
            _jumpState = new JumpState(this);
            _doubleJumpState = new JumpState(this);
            _stateMachine = new StateMachine(_groundedState);
            _stateMachine.AddTransition(_groundedState,"Jump", _jumpState);
            _stateMachine.AddTransition(_jumpState,"DoubleJump", _doubleJumpState);
        }
        //On Enable, set inputs
        private void OnEnable()
        {
            if (moveInput?.action != null)
            {
                moveInput.action.started += HandleMoveInput;
                moveInput.action.performed += HandleMoveInput;
                moveInput.action.canceled += HandleMoveInput;
            }
            if (jumpInput?.action != null)
                jumpInput.action.performed += HandleJumpInput;
        }
        //On Disable, remove inputs
        private void OnDisable()
        {
            if (moveInput?.action != null)
            {
                moveInput.action.performed -= HandleMoveInput;
                moveInput.action.canceled -= HandleMoveInput;
            }
            if (jumpInput?.action != null)
                jumpInput.action.performed -= HandleJumpInput;
        }
        //Use the state machine bool to control if the player is airborne or not via states.
        private void HandleMoveInput(InputAction.CallbackContext ctx)
        {
            CurrentDirection = ctx.ReadValue<Vector2>().ToHorizontalPlane();
            Character.SetDirection(CurrentDirection);
        }
        //On handle Input, try to execute the handle inside the states.
        private void HandleJumpInput(InputAction.CallbackContext ctx)
        {
            //This also works without a bool, but only if "TryTransition(DoubleJump)" initializes first.
            var state = IsAirborne ? "DoubleJump" : "Jump";
            _stateMachine.TryTransition(state);
            _stateMachine.TryHandleInput();
        }
        //On Collision near ground, change state to ground.
        private void OnCollisionEnter(Collision other)
        {
            foreach (var contact in other.contacts)
            {
                if (Vector3.Angle(contact.normal, Vector3.up) < 5)
                {
                   _stateMachine.ChangeState(_groundedState);
                   IsAirborne = false;
                }
            }
        }
    }
}