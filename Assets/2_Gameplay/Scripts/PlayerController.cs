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
            A State Machine is created with "CurrentState" handling using the SetState and TryHandleInput methods.
            The "State" classes contain the OnEnterState, OnHandle, and OnEndState methods to control their states.
            I would consider this a Strategy Pattern, but because a "Context" is required within the States,
            In this case, Character and StateMachine, it cannot be considered a Strategy Pattern.
            Some States do not have OnEnterState, OnHandle or OnEndState implemented, but they are still part of the 
            State Machine and can be used in the future to implement other features (animations, sounds, etc.).
            The State Machine CAN be used for every class that implements the "Character" class component.
        */
        private Character _character;
        [SerializeField] private StateMachine _stateMachine;
        //Creation of NewStateMachine for the Character.
        private void Awake()
        {
            _character = GetComponent<Character>();
            _stateMachine = new StateMachine(_character);
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
            var direction = ctx.ReadValue<Vector2>().ToHorizontalPlane();
            if (_stateMachine.IsAirborne)
                direction *= airborneSpeedMultiplier;
            _character?.SetDirection(direction);
        }
        //On handle Input, try to execute handle inside the states.
        private void HandleJumpInput(InputAction.CallbackContext ctx)
        {
            _stateMachine.TryHandleInput();
        }
        //On Collision near ground, change state to ground.
        private void OnCollisionEnter(Collision other)
        {
            foreach (var contact in other.contacts)
            {
                if (Vector3.Angle(contact.normal, Vector3.up) < 5)
                {
                    _stateMachine.SetState(new GroundedState(_character, _stateMachine));
                }
            }
        }
    }
}