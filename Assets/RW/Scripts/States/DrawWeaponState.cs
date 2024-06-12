using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class DrawWeaponState : GroundedState
    {
        private bool drawingAnimationFinished;
        public DrawWeaponState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public void Start()
        {
            drawingAnimationFinished = false;
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("DrawWeaponState entered.");
            character.SetAnimationBool(character.isMelee, true); //"call" the animator and pass in the isMelee parameter
            character.SetAnimationBool(character.drawMelee, true); //"call" the animator by passing in the parameter name (this is the drawmelee animation)
            character.Equip(character.MeleeWeapon); //call the equip function from the character script
            drawingAnimationFinished = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Debug.Log("Logic update");
            if (drawingAnimationFinished)
            {
                character.isWeaponOut = true;
                stateMachine.ChangeState(character.standing);
            }

        }
        public override void Exit()
        {
            base.Exit();
            Debug.Log("DrawWeaponState exiting.");
            character.SetAnimationBool(character.drawMelee, false); //set the bool to false

        }
    }
}
