using Panda.Examples.PlayTag;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class PatrollingState : EnemyState
    {
        NavMeshAgent navAgent;

        public PatrollingState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        void PlayerWithinDetectRange()
        {
            float distance = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);
            //Debug.Log("Checking if player is within range " + distance);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        //bool RandomPoint(Vector3 center, float range, out Vector3 result)
        //{
            
        //}

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Enemy has entered its Patrol state");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            PlayerWithinDetectRange();
            if (enemy.character.CurrentState is DuckingState)
            {
                Debug.Log("The character is ducking and cannot be detected");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

