using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class AnimatorController : MonoBehaviour
    {
        enum AnimatorState
        {
            L0A0 = 0,
            L0A1 = 1,
            L0A2 = 2,
            L0A3 = 3,
            L1A0 = 10,
            L1A1 = 11,
            L1A2 = 12,
            L2A0 = 20,
            L2A1 = 21,
            L3A0 = 30
        }

        [SerializeField] private Animator playerAnimator;
        private GameObject parent;
        private Rigidbody2D rigidbody2D1;
        private float currentVelocityX;
        private float currentVelocityY;
        private AnimatorState currentState;
        private bool velocityXNegative = false;




        public BodyManager bManager;
        
        void Start()
        {
            parent = transform.parent.gameObject;
            rigidbody2D1 = parent.GetComponent<Rigidbody2D>();

            bManager = parent.GetComponent<BodyManager>();
            if (bManager != null)
            {
                bManager.LimbsChanged += StateUpdate;
            }

        }

        void Update()
        {
            currentVelocityY = rigidbody2D1.linearVelocity.y;

            currentVelocityX = rigidbody2D1.linearVelocity.x;
            setVelocityY();

            if (velocityXNegative != currentVelocityX < 0)
            {
                velocityXNegative = !velocityXNegative;
                Flip();

            }

        }

        private void StateUpdate(int legs, int arms)
        {
            currentState = (AnimatorState)(10 * legs + arms);
            setTrigger(currentState);
        }

        private void setVelocityY()
        {
            playerAnimator.SetFloat("velocityY", Mathf.Abs(currentVelocityY));
        }

        private void setTrigger(AnimatorState state)
        {
            playerAnimator.SetTrigger(state.ToString());
           
        }

        private void Flip()
        {
            var scale = parent.transform.localScale;
            scale.x = scale.x * -1;
            parent.transform.localScale = scale;
        }


        private void OnEnable()
        {

            if (bManager != null)
            {
                bManager.LimbsChanged += StateUpdate;
            }
        }

        private void OnDisable()
        {

            if (bManager != null)
            {
                bManager.LimbsChanged -= StateUpdate;
            }
        }
    }
}