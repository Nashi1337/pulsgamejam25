using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions _playerInputActions;
    private CharacterController _characterController;

    private Vector2 _moveInput;


    private void Awake()
    {
        _playerInputActions = new InputSystem_Actions();
        _characterController = GetComponent<CharacterController>();

        _playerInputActions.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _playerInputActions.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
        _playerInputActions.Player.Jump.performed += ctx => Jump();
    }

    private void OnEnable()
    {
        _playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Player.Disable();
    }

    private void Update()
    {
        GroundCheck();
        Move();
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        _characterController.Move(moveDirection);
    }

    private void Jump()
    {
        _characterController.Jump();
    }
    
    private void GroundCheck()
    {
        //_characterController.GroundCheck();
    }
}