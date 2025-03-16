using UnityEngine;

namespace Player.Legs._2
{
    public class Leg2: MonoBehaviour, LegComponent
    {
        [SerializeField] private LegStats _stats;
        public bool isGrounded { get; set; }

        
        public LegStats Stats
        {
            get => _stats;
            set => _stats = value;
        }

    }
}