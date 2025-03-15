using System;
using System.Collections.Generic;
using System.Linq;
using Player.Arms;
using Player.Legs;
using UnityEngine;

namespace Player
{
    public class BodyManager : MonoBehaviour
    {
        public GameObject[] armPrefabs;
        public GameObject[] legPrefabs;

        public int maxParts = 0;
        private int unequippedComponents = 0;

        public Transform armTransform;
        public Transform legTransform;

        public int PartsRemaining => maxParts - currentArmIndex - currentLegIndex;

        [ReadOnly] public int currentLegIndex = 0;
        [ReadOnly] public int currentArmIndex = 0;

        [ReadOnly] public GameObject currentArm;
        [ReadOnly] public GameObject currentLeg;

        [ReadOnly] public ArmComponent armScript;
        [ReadOnly] public LegComponent legScript;

        private void Start()
        {
            if (
                TrySwitchToArm(currentArmIndex) &&
                TrySwitchToLeg(currentLegIndex))
            {
                
            }
            else
            {
                throw new Exception("BodyManager: Init Failed");
            }
        }

        public bool CanSwitchToLeg(int legIndex)
        {
            if (legIndex < 0 || legIndex > maxParts)
                return false;
            return (legIndex + currentArmIndex) <= maxParts;
        }

        public bool CanSwitchToArm(int armIndex)
        {
            if (armIndex < 0 || armIndex > maxParts)
                return false;
            return (armIndex + currentLegIndex) <= maxParts;
        }

        public bool TrySwitchToLeg(int legIndex)
        {
            if (!CanSwitchToLeg(legIndex))
            {
                return false;
            }
            
            if(currentLeg != null)
                Destroy(currentLeg);

            currentLegIndex = legIndex;
            currentLeg = Instantiate(legPrefabs[legIndex], legTransform);
            legScript = currentLeg.GetComponent<LegComponent>();
            return true;
        }

        public bool TrySwitchToArm(int armIndex)
        {
            if (!CanSwitchToArm(armIndex))
            {
                return false;
            }
            
            if (currentArm != null)
                Destroy(currentArm);

            currentArmIndex = armIndex;
            currentArm = Instantiate(armPrefabs[armIndex], armTransform);
            armScript = currentArm.GetComponent<ArmComponent>();
            return true;
        }

        public void AddComponent()
        {
            maxParts++;
            unequippedComponents++;
        }

        public void EquipArm()
        {
            if (unequippedComponents > 0)
            {
                unequippedComponents--;
                currentArmIndex++;
            }else if (unequippedComponents == 0)
            {
                unequippedComponents++;
                currentArmIndex--;
            }
        }
    }
}