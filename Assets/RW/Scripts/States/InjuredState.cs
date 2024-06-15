using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class InjuredState : GroundedState
    {
        private bool injuredAnimationDone; //boolean that keeps track of the swing animation
        private float injuredAnimationTime = .5f;
        public int injuredParam => Animator.StringToHash("Injured");  //access the animator by storing the name of the parameter as swingMelee
        public InjuredState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }


        private IEnumerator InjuredCoroutine()
        {
            yield return new WaitForSeconds(injuredAnimationTime);
            injuredAnimationDone = true;
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("InjuredState entered");
            injuredAnimationDone = false;
            character.TriggerAnimation(injuredParam);
            character.StartCoroutine(InjuredCoroutine());
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Debug.Log("InjuredState Logic");
            if (injuredAnimationDone)
            {
                stateMachine.ChangeState(character.standing);
            }
            
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("InjuredState exiting");
        }
    }
}