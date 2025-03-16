using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputSystem_Actions _playerInputActions;
    private CharacterController _characterController;

    private Vector2 _moveInput;

    private bool disabled = false;


    private void Awake()
    {
        _playerInputActions = new InputSystem_Actions();
        _characterController = GetComponent<CharacterController>();

        _playerInputActions.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _playerInputActions.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
        _playerInputActions.Player.Jump.started += ctx => Jump();
        _playerInputActions.Player.Interact.started += ctx => Interact();
        _playerInputActions.Player.SwitchLeg.started += ctx => SwitchLeg();
        _playerInputActions.Player.SwitchArm.started += ctx => SwitchArm();
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
        if(!disabled)
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

    private void Interact()
    {
        _characterController.Interact();
    }

    private void SwitchArm()
    {
        _characterController.SwitchArm();
    }

    private void SwitchLeg()
    {
        _characterController.SwitchLeg();
    }

    public void ToggleMovement()
    {
        disabled = !disabled;
        Debug.Log("Movement now disabled? " + disabled);
    }
}