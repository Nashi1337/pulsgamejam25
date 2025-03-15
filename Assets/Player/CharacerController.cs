using System;
using Player;
using Player.Arms;
using Player.Legs;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public float gravity = -9.81f;
    private Vector3 _velocity;
    private BodyManager _bodyManager;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bodyManager = GetComponent<BodyManager>();
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
        _bodyManager.legScript.GroundCheck(_rb);
    }

    // private void ApplyGravity()
    // {
    //     _velocity.y += gravity * Time.deltaTime;
    //     transform.position += _velocity * Time.deltaTime;
    // }
}