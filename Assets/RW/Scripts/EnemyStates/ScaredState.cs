using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class ScaredState : EnemyState
    {
        Animator animator;
        public int scared => Animator.StringToHash("Scared");
        public ScaredState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Enemy scared");

            animator = enemy.anim;
            animator.SetTrigger(scared);



        }
    }
}
