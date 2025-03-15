
using UnityEngine;

namespace Player.Legs
{
    public interface LegComponent
    {
        
        public void Move(Vector2 direction, Rigidbody2D rb);
        public void Jump( Rigidbody2D rb);
        public void GroundCheck(Rigidbody2D rb);
    }
}