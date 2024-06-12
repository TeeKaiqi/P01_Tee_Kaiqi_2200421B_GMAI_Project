using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SheathWeaponState : GroundedState
    {
        private bool sheathMeleeAnimationFinish;

        public SheathWeaponState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public void Start()
        {
            sheathMeleeAnimationFinish = false;
        }

        public override void Enter()
        {
            Debug.Log("SheathWeaponState entered.");
            character.SetAnimationBool(character.sheathMelee, true);
            character.Unequip();
            sheathMeleeAnimationFinish = true;
        }

        public override void LogicUpdate()
        {
            Debug.Log("SheathWeaponState Logic Entered.");
            base.LogicUpdate();
            if (sheathMeleeAnimationFinish)
            {
                character.isWeaponOut = false;
                stateMachine.ChangeState(character.standing);
            }
        }
        public override void Exit()
        {
            Debug.Log("SheathWeaponState Exit entered");
            character.SetAnimationBool(character.sheathMelee, false);
        }
    }
}