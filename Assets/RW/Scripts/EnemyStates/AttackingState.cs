using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class AttackingState : EnemyState
    {
        public NavMeshAgent navAgent;
        public float attackRadius;
        public AttackingState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
        // Start is called before the first frame update
        public override void Enter()
        {
            base.Enter();
            navAgent = enemy.navAgent;
        }

        public override void Exit() 
        {
            base.Exit();
        }
    }
}