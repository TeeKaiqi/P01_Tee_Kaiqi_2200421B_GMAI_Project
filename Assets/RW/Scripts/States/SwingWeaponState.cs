using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SwingWeaponState : GroundedState
    {
        private bool belowCeiling;
        private bool crouchHeld;

        public SwingWeaponState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }
        public override void Enter()
        {
            Debug.Log("SwingWeaponState entered.");
        }
    }
}