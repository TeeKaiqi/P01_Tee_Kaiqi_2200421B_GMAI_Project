using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class ScaredState : EnemyState
    {
        Animator animator;
        CreatureTasks creatureTasks;
        public int scared => Animator.StringToHash("Scared");
        private int scaredAnimationTime = 2;
        private bool scaredAnimationPlayed;
        public ScaredState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
        private IEnumerator ScaredCoroutine()
        {
            yield return new WaitForSeconds(scaredAnimationTime);

            scaredAnimationPlayed = true;
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Enemy scared");
            scaredAnimationPlayed = false;
            animator = enemy.anim;
            creatureTasks = enemy.creatureTasks;
            animator.SetTrigger(scared); //trigger scared animation
            enemy.StartCoroutine(ScaredCoroutine()); //start the coroutine
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (scaredAnimationPlayed)
            {
                stateMachine.ChangeEnemyState(enemy.patrolState); //change state to patrol once the scared animation is done
            }
        }
        public override void Exit()
        {
            base.Exit();
            creatureTasks.tauntActionDone = false; //set the creatureTasks tauntaction to false so the enemy isnt constantly entering its scaredstate
        }
    }
}
