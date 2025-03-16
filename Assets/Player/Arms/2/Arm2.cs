using UnityEngine;

namespace Player.Arms._2
{
    public class Arm2 : MonoBehaviour, ArmComponent
    {
        public float pushForce = 5f;
        public bool canPush = false;
        
        public void Use(Vector2 pointer)
        {
            throw new System.NotImplementedException();
        }

        public void SetPushability(int armIndex)
        {
            if (armIndex >= 2)
                canPush = true;
            else
                canPush = false;
        }
        
        public void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Pushable") && canPush)
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 pushDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
                    rb.linearVelocity = new Vector2(pushDirection.x * pushForce, rb.linearVelocity.y);
                }
            }
        }
    }
}