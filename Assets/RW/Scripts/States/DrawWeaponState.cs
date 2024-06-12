using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class DrawWeaponState : GroundedState
    {
        private bool drawingAnimationFinished; //boolean that keeps track of it the animation progress
        public int drawMelee => Animator.StringToHash("DrawMelee"); //access the drawMelee parameter from the animator so that I can play the animation from DrawWeaponState

        public DrawWeaponState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public void Start()
        {
            drawingAnimationFinished = false; //set the variable to false so it can be reused in future when this state is called again
        }
        public override void Enter()
        {
            base.Enter();
            //Debug.Log("DrawWeaponState entered.");
            character.SetAnimationBool(character.isMelee, true); //"call" the animator and pass in the isMelee parameter
            character.SetAnimationBool(drawMelee, true); //"call" the animator by passing in the parameter name (this is the drawmelee animation)
            character.Equip(character.MeleeWeapon); //call the equip function from the character script
            drawingAnimationFinished = true; //set the variable to true, will be used to tell the script when to changestate/exit
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            //Debug.Log("Logic update");
            if (drawingAnimationFinished) //if the draw animation is done it is time to switch states
            {
                character.isWeaponOut = true; //change the boolean to true, this is to ensure the user can sheath the weapon if the weapon is out 
                stateMachine.ChangeState(character.standing); //change the character state back to standing so that the user can walk like normal
            }

        }
        public override void Exit()
        {
            base.Exit();
            //Debug.Log("DrawWeaponState exiting.");
        }
    }
}
