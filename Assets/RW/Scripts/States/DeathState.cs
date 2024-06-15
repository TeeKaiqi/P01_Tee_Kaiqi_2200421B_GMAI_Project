using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class DeathState : GroundedState
    {
        public int death => Animator.StringToHash("Dead"); //access to the death parameter in the animator
        public DeathState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            character.SetAnimationBool(death, true); //set the death aimation to true
        }

        public override void Exit() 
        { 
            base.Exit();
        }
    }
}
