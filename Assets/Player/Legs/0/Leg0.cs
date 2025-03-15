using UnityEngine;

namespace Player.Legs._0
{
    public class Leg0 : MonoBehaviour, LegComponent
    {
        const double NotMovingDelta = 10e-3;

        
        
        public float _dampening = 3f;
        public float _jumpHeight = 5f;
        public float _maxSpeed = 5f;
        
        
        public void Move(Vector2 direction, Rigidbody2D rb)
        {
            float targetVelocity = direction.x * _maxSpeed;

            if (direction.x == 0)
            {
                rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, targetVelocity, Time.deltaTime * _dampening);

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

        public void Jump(Rigidbody2D rb, bool isGrounded)
        {
            if(isGrounded)
                rb.AddForce(new Vector2(0, _jumpHeight), ForceMode2D.Impulse);
        }
    }
}