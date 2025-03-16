using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "LegStats", menuName = "Scriptable Objects/LegStats")]
    public class LegStats : ScriptableObject
    {
        public float maxSpeed;
        public float groundDampening;
        public float acceleration;
        public float jumpHeight;
    }
}
