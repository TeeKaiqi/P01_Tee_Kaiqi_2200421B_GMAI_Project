using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class DeadState : EnemyState
    {
        Animator animator;
        public int dead => Animator.StringToHash("Dead");
        public DeadState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
        // Start is called before the first frame update
        public override void Enter()
        {
            base.Enter();
            Debug.Log("Enemy dead");
            animator = enemy.anim;
            animator.SetBool(dead, true);

        }

        public override void Exit() 
        {
            base.Exit();
        }
    }
}