using System;
using Player;
using Player.Arms;
using Player.Legs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public float gravity = -9.81f;
    private Vector3 _velocity;
    private BodyManager _bodyManager;
    bool _isGrounded;
    
    private Rigidbody2D _rb;
    private BoxCollider2D _col;
    
    private GameObject interactableObject;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bodyManager = GetComponent<BodyManager>();
    }

    private void Update()
    {
        GroundCheck();
        //ApplyGravity();
    }

    public void Jump()
    {
        _bodyManager.legScript.Jump(_rb);
    }

    public void Move(Vector2 move)
    {
        _bodyManager.legScript.Move(move, _rb);
    }

    public void GroundCheck()
    {
        _bodyManager.legScript.isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * 1.1f);
    }

    // private void ApplyGravity()
    // {
    //     _velocity.y += gravity * Time.deltaTime;
    //     transform.position += _velocity * Time.deltaTime;
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Death"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        if (other.gameObject.CompareTag("Collectable"))
        {
            _bodyManager.AddComponent();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("SwingHook"))
        {
            interactableObject = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            interactableObject = null;
        }

        if (other.gameObject.CompareTag("SwingHook"))
        {
            interactableObject = null;
        }
    }

    public void Interact()
    {
        if (interactableObject != null)
        {
            if (interactableObject.CompareTag("SwingHook") && _bodyManager.GetArmIndex() > 0)
            {
                GetComponent<PlayerMovement>().ToggleMovement();
                _rb.linearVelocityX = 0;
                GetComponent<PlayerSwing>().Swing(interactableObject.transform);
            }
        }
    }

    public void SwitchArm()
    {
        _bodyManager.EquipArm();
    }

    public void SwitchLeg()
    {
        _bodyManager.EquipLeg();
    }
    
    
}