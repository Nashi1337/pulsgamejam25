using UnityEngine;

namespace Player.Legs._1
{
    public class Leg1: MonoBehaviour, LegComponent
    {
        [SerializeField] private LegStats _stats;


        public LegStats Stats
        {
            get => _stats;
            set => _stats = value;
        }
    }
}