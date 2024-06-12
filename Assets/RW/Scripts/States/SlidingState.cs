using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SlidingState : GroundedState
    {
        private bool slidingAnimationDone; //boolean that keeps track of the swing animation
        private bool slideHeld;
        public int slidingParam => Animator.StringToHash("Slide");  //access the animator by storing the name of the parameter as swingMelee
        public SlidingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public void Start()
        {
            slidingAnimationDone = false;
            slideHeld = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            slideHeld = Input.GetKey(KeyCode.LeftControl);
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("SlidingState entered");
            character.SetAnimationBool(slidingParam, true);
            slidingAnimationDone = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Debug.Log("SlideState Logic");
            if (!(slideHeld && slidingAnimationDone))
            {
                stateMachine.ChangeState(character.standing);
            }
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("SlidingState exiting");
            character.SetAnimationBool(slidingParam, false);
        }
    }
}