using System;
using System.Collections.Generic;
using System.Linq;
using Player.Arms;
using Player.Arms._2;
using Player.Legs;
using Sound;
using UnityEngine;

namespace Player
{
    public class BodyManager : MonoBehaviour
    {
        public GameObject[] armPrefabs;
        public GameObject[] legPrefabs;

        [SerializeField]private int unequippedComponents = 0;

        public Transform armTransform;
        public Transform legTransform;

        [ReadOnly] public int currentLegIndex = 0;
        [ReadOnly] public int currentArmIndex = 0;

        [ReadOnly] public GameObject currentArm;
        [ReadOnly] public GameObject currentLeg;

        [ReadOnly] public ArmComponent armScript;
        [ReadOnly] public LegComponent legScript;
        
        public AudioSource audioSource;

        public UIManager uiManager;
        private LayerMask pushableLayer;

        private void Start()
        {
            pushableLayer = LayerMask.NameToLayer("Pushable");
            currentArm = Instantiate(armPrefabs[currentArmIndex], armTransform);
            armScript = currentArm.GetComponent<ArmComponent>();
            currentLeg = Instantiate(legPrefabs[currentLegIndex], legTransform);
            legScript = currentLeg.GetComponent<LegComponent>();
            
            audioSource = GetComponent<AudioSource>();

            UpdatePushCollision();
            UpdateUILabels();

        }

        public bool TrySwitchToLeg(int legIndex)
        {
            var available = unequippedComponents + currentLegIndex;

            if ((legIndex < 0 || legIndex > available))
            {
                return false;
            }

            if (currentLeg != null)
                Destroy(currentLeg);
            unequippedComponents = unequippedComponents + currentLegIndex -legIndex;

            currentLegIndex = legIndex;

            currentLeg = Instantiate(legPrefabs[legIndex], legTransform);
            legScript = currentLeg.GetComponent<LegComponent>();
            return true;
        }

        public bool TrySwitchToArm(int armIndex)
        {
            var available = unequippedComponents + currentArmIndex;

            if ((armIndex < 0 || armIndex > available))
            {
                return false;
            }

            if (currentArm != null)
                Destroy(currentArm);
            unequippedComponents = unequippedComponents + currentArmIndex - armIndex;

            currentArmIndex = armIndex;
            currentArm = Instantiate(armPrefabs[armIndex], armTransform);
            armScript = currentArm.GetComponent<ArmComponent>();
            return true;
        }

        public void AddComponent()
        {
            unequippedComponents++;
            UpdateUILabels();
        }

        public void EquipArm()
        {
            SoundPitcher.PitchRandom(audioSource,0.9f,1.1f);
            audioSource.Play();
            
            if (!TrySwitchToArm(currentArmIndex+1 % armPrefabs.Length))
            {
                var res = TrySwitchToArm(0);
                if (!res)
                {
                    throw new Exception("BodyManager: Switch Failed");
                }
            }

            UpdatePushCollision();
            UpdateUILabels();
        }

        private void UpdatePushCollision()
        {
            if (currentArmIndex >= 2)
            {
                gameObject.GetComponent<CapsuleCollider2D>().excludeLayers &= ~(1 << pushableLayer);
            }
            else
            {
                gameObject.GetComponent<CapsuleCollider2D>().excludeLayers |= (1 << pushableLayer);
            }
        }

        public void EquipLeg()
        {
            SoundPitcher.PitchRandom(audioSource,0.9f,1.1f);
            audioSource.Play();
            
            if (!TrySwitchToLeg(currentLegIndex+1 % legPrefabs.Length))
            {
                var res = TrySwitchToLeg(0);
                if (!res)
                {
                    throw new Exception("BodyManager: Switch Failed");
                }
            }

            UpdateUILabels();
        }

        public int GetArmIndex()
        {
            return currentArmIndex;
        }

        public int GetLegIndex()
        {
            return currentLegIndex;
        }

        void UpdateUILabels()
        {
            uiManager.UpdateArmLabel(currentArmIndex);
            uiManager.UpdateLegLabel(currentLegIndex);
            uiManager.UpdateUnusedLabel(unequippedComponents);
        }
    }
}