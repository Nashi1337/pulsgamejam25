using System.Collections.Generic;
using Player.Arms;
using Player.Legs;
using UnityEngine;

namespace Player
{
    public class BodyManager : MonoBehaviour
    {
        public GameObject[] armPrefabs;
        public GameObject[] legPrefabs;

        public int maxParts;

        public Transform armTransform;
        public Transform legTransform;

        public int PartsRemaining => maxParts - currentArmIndex - currentLegIndex;

        [ReadOnly] public int currentLegIndex;
        [ReadOnly] public int currentArmIndex;

        [ReadOnly] public GameObject currentArm;
        [ReadOnly] public GameObject currentLeg;

        [ReadOnly] public ArmComponent armScript;
        [ReadOnly] public LegComponent legScript;

        public bool CanSwitchToLeg(int legIndex)
        {
            if (legIndex < 0 || legIndex >= maxParts)
                return false;
            return legIndex + currentArmIndex <= maxParts;
        }

        public bool CanSwitchToArm(int armIndex)
        {
            if (armIndex < 0 || armIndex >= maxParts)
                return false;
            return armIndex + currentArmIndex <= maxParts;
        }

        public bool TrySwitchToLeg(int legIndex)
        {
            if (!CanSwitchToLeg(legIndex))
            {
                return false;
            }

            currentLegIndex = legIndex;
            currentLeg = Instantiate(legPrefabs[legIndex], legTransform);
            return true;
        }

        public bool TrySwitchToArm(int armIndex)
        {
            if (!CanSwitchToArm(armIndex))
            {
                return false;
            }

            currentArmIndex = armIndex;
            currentArm = Instantiate(armPrefabs[armIndex], armTransform);
            return true;
        }
    }
}