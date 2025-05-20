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
            Se intentó realizar un State Machine con manejo de "CurrentState" con
            clases "State" que contengan los metodos OnStartState, OnHandleState, OnEndState, pero debido
            a la dependencia de "InputAction.CallbackContext" se hizo imposible, por lo que se optó por
            hacerlo con un State Machine que se encarga de manejar el estado de "Grounded", "FirstJump",
            "SecondJump", el cual se puede expandir fácilmente en caso de que se requiera agregar de otros 
            estados aparte.
            Lo consideraria Strategy Pattern, pero debido a que se requiere de un "Contexto", en este caso Player
            Controller, no se lo podria considerar Strategy Pattern. 
        */
        private Character _character;
        private StateMachine _stateMachine;

        private void Awake()
        {
            _character = GetComponent<Character>();
            _stateMachine = new StateMachine(_character);
        }

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

        private void HandleMoveInput(InputAction.CallbackContext ctx)
        {
            var direction = ctx.ReadValue<Vector2>().ToHorizontalPlane();
            if (_stateMachine.IsAirborne)
                direction *= airborneSpeedMultiplier;
            _character?.SetDirection(direction);
        }

        private void HandleJumpInput(InputAction.CallbackContext ctx)
        {
            _stateMachine.TryJump();
        }

        private void OnCollisionEnter(Collision other)
        {
            foreach (var contact in other.contacts)
            {
                if (Vector3.Angle(contact.normal, Vector3.up) < 5)
                {
                    _stateMachine.ResetJumpState();
                }
            }
        }
    }
}