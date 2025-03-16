using System;
using UnityEngine;

namespace Player.Arms._3
{
    public class Arm3 : MonoBehaviour, ArmComponent
    {
        public GameObject boomerangPrefab;
        public float throwSpeed = 10f;
        public float returnSpeed = 10f;

        private GameObject currentBoomerang;
        private Transform playerTransform;

        private void Start()
        {
            playerTransform = transform;
        }

        public void Use(Vector2 pointer)
        {
            throw new System.NotImplementedException();
        }

        public void Throw(Transform transform)
        {
            playerTransform = transform;
            if (currentBoomerang != null) return;
            
            currentBoomerang = Instantiate(boomerangPrefab, playerTransform.position, Quaternion.identity);
            
            Rigidbody2D rb = currentBoomerang.GetComponent<Rigidbody2D>();
            Vector2 throwDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            
            rb.linearVelocity = throwDirection * throwSpeed;

            Boomerang boomerang = currentBoomerang.AddComponent<Boomerang>();
            boomerang.Initialize(this, playerTransform, throwSpeed, returnSpeed);
        }

        public void OnBoomerangDestroyed()
        {
            currentBoomerang = null;
        }
    }
}