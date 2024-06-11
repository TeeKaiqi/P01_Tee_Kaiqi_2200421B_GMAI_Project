using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SheathWeaponState : GroundedState
    {
        private bool belowCeiling;
        private bool crouchHeld;

        public SheathWeaponState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }
        public override void Enter()
        {
            Debug.Log("SheathWeaponState entered.");
        }
    }
}