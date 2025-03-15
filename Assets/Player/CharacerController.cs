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
    }

    public void Jump()
    {
        _bodyManager.legScript.Jump(_rb, _isGrounded);
    }

    public void Move(Vector2 move)
    {
        _bodyManager.legScript.Move(move, _rb);
    }

    public void GroundCheck()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, LayerMask.GetMask("Ground"));
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
        
        if (other.gameObject.CompareTag("Interactable"))
        {
            interactableObject = other.gameObject;
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
            if (interactableObject.CompareTag("Interactable"))
            {
                _bodyManager.AddComponent();
                DestroyImmediate(interactableObject);
                interactableObject = null;
            }
            else if (interactableObject.CompareTag("SwingHook"))
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
        //_bodyManager.EquipLeg();
    }
    
    
}