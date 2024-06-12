using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SheathWeaponState : GroundedState
    {
        private bool sheathMeleeAnimationFinish; //boolean that keeps track of the animation status, used for changing states

        public SheathWeaponState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public void Start()
        {
            sheathMeleeAnimationFinish = false; //set the variable to false in start
        }

        public override void Enter()
        {
            base.Enter();
            //Debug.Log("SheathWeaponState entered.");
            character.SetAnimationBool(character.sheathMelee, true); //set the animator sheathmelee bool to true so that the animation can play
            character.Unequip(); //unequip the weapon by calling the function to destroy the weapon in character
            sheathMeleeAnimationFinish = true; //set the animation bool to finish so that the logic update can change state
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            //Debug.Log("SheathWeaponState Logic Entered.");
            if (sheathMeleeAnimationFinish) //if the animation is done playing, following logic ensues
            {
                character.isWeaponOut = false; //set the boolean to false so that the character can draw their sword again after this
                stateMachine.ChangeState(character.standing); //change the character state to standing
            }
        }
        public override void Exit()
        {
            base.Exit();
            //Debug.Log("SheathWeaponState Exit entered");
            character.SetAnimationBool(character.sheathMelee, false); //set the animator sheath boolean to false so that it can be activated again in the future
        }
    }
}