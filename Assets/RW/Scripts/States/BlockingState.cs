using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class BlockingState : GroundedState
    {
        public bool blockAnimationPlayed;
        private bool blockHeld;
        public int block => Animator.StringToHash("Block"); //access to the death parameter in the animator
        public BlockingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public void Start()
        {
            blockAnimationPlayed = false;
            blockHeld = false;
        }
        public override void Enter()
        {
            base.Enter();
            character.SetAnimationBool(block, true);
            blockAnimationPlayed = true;
        }
        public override void HandleInput()
        {
            base.HandleInput();
            blockHeld = Input.GetKey(KeyCode.E);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Debug.Log("SlideState Logic");
            if (!(blockHeld && blockAnimationPlayed))
            {
                stateMachine.ChangeState(character.standing);
            }
        }

        public override void Exit() 
        { 
            base.Exit();
            Debug.Log("BlockState exiting");
            character.SetAnimationBool(block, false);
        }
    }
}
