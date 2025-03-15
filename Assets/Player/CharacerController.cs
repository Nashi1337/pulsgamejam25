using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public float gravity = -9.81f;

    private Vector3 _velocity;

    public void Jump()
    {
    }

    public void Move(Vector2 move)
    {
    }

    private void Update()
    {
    }

    private void ApplyGravity()
    {
        _velocity.y += gravity * Time.deltaTime;
        transform.position += _velocity * Time.deltaTime;
    }
}