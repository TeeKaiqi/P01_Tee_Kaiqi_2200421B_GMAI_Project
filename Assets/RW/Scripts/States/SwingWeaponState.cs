using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SwingWeaponState : GroundedState
    {
        private bool swingAnimationDone; //boolean that keeps track of the swing animation
        public int swingMelee => Animator.StringToHash("SwingMelee"); //access the animator by storing the name of the parameter as swingMelee
        public void Start()
        {
            swingAnimationDone = false; //set the variable to false
        }

        public SwingWeaponState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //Debug.Log("SwingWeaponState entered.");
            character.TriggerAnimation(swingMelee); //trigger the swing animation
            swingAnimationDone = true; //set the animation bool to true
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (swingAnimationDone) //when the bool is true, following logic ensues 
            {
                //Debug.Log("SwingState Logic reached.");
                stateMachine.ChangeState(character.standing); //change to standing state 
            }
        }

        public override void Exit()
        {
            base.Exit();
            //Debug.Log("SwingState exit logic reached.");
        }
    }
}