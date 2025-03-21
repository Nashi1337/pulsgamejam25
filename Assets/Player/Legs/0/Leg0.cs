﻿using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Player.Legs._0
{
    public class Leg0 :   MonoBehaviour, LegComponent
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