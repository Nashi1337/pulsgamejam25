using UnityEngine;
using Vector2 = System.Numerics.Vector2;

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
            Debug.Log("colliding");
            if (collision.gameObject.CompareTag("Pushable") && canPush)
            {
                Debug.Log("object is pushable and i can push");
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 pushDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
                    rb.linearVelocity = new UnityEngine.Vector2(pushDirection.X * pushForce, rb.linearVelocity.y);
                }
            }
        }
    }
}