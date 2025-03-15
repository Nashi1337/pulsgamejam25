using System.Numerics;

namespace Player.Legs
{
    public interface LegComponent
    {
        public void Move(Vector2 direction);
    }
}