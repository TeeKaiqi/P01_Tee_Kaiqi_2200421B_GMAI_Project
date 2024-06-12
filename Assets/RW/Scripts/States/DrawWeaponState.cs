using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class DrawWeaponState : GroundedState
    {
        public DrawWeaponState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("DrawWeaponState entered.");
           
            character.SetAnimationBool(character.isMelee, true);
            character.Equip(character.MeleeWeapon); //call the equip function from the character script
        }
    }
}
