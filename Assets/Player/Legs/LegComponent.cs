
using Unity.VisualScripting;
using UnityEngine;

namespace Player.Legs
{
    public interface LegComponent 
    {
    const double NotMovingDelta = 10e-3;
    LegStats Stats { get; set; }
    bool isGrounded { get; set; }

    public void Move(Vector2 direction, Rigidbody2D rb)
    {
        float targetVelocity = direction.x * Stats.maxSpeed;

        if (direction.x == 0)
        {
            rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, targetVelocity, Time.deltaTime * Stats.groundDampening);

            if (Mathf.Abs(rb.linearVelocityX) < NotMovingDelta)
            {
                rb.linearVelocityX = 0;
            }
        }
        else
        {
            rb.linearVelocityX = targetVelocity;
        }

        rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocity.y);
    }

        public void Jump(Rigidbody2D rb)
        {
            if (isGrounded)
                rb.linearVelocityY = Stats.jumpHeight;
        }

    }
}