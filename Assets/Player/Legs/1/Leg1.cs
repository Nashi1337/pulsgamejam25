using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player.Legs._1
{
    public class Leg1 : MonoBehaviour, LegComponent
    {
        [SerializeField] private LegStats _stats;
        const float JumpWait = .1f; 
        public LegStats Stats
        {
            get => _stats;
            set => _stats = value;
        }

        public bool isGrounded { get; set; }

        public void Move(Vector2 direction, Rigidbody2D rb)
        {
            
            //copy and paste because LegComponent is interface so no base.Method
            //if i try to make LegComponent abstract class i get a million errors in CharacterController and just
            //dont care anymore
            
            float targetVelocity = direction.x * Stats.maxSpeed;

            if (direction.x == 0)
            {
                rb.linearVelocityX = Mathf.Lerp(rb.linearVelocityX, targetVelocity, Time.deltaTime * Stats.groundDampening);

                if (Mathf.Abs(rb.linearVelocityX) < LegComponent.NotMovingDelta)
                {
                    rb.linearVelocityX = 0;
                }
            }
            else
            {
                rb.linearVelocityX = targetVelocity;
            }

            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocity.y);

            if(isGrounded )
            {
                rb.linearVelocityY = Stats.jumpHeight;
            }
        }

        public void Jump(Rigidbody2D rb)
        {
            return;
        }
    }
}