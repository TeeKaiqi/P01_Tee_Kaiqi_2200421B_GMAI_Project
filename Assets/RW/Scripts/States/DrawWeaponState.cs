using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class DrawWeaponState : GroundedState
    {
        private bool belowCeiling;
        private bool crouchHeld;

        public DrawWeaponState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }
        public override void Enter()
        {
            Debug.Log("DrawWeaponState entered.");
        }
    }
}
