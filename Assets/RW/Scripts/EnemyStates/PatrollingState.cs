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
        public float range = 10f;
        public PatrollingState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {

        }

        void Start()
        {
            navAgent = enemy.GetComponent<NavMeshAgent>();
        }

        void SetRandomDestination()
        {
            Vector3 randomDestination;
            if (RandomPoint(enemy.transform.position, range, out randomDestination))
            {
                navAgent.SetDestination(randomDestination);
            }
        }
        void PlayerWithinDetectRange()
        {
            float distance = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);
            //Debug.Log("Checking if player is within range " + distance);
        }

        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
            result = Vector3.zero;
            return false;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Enemy has entered its Patrol state");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            PlayerWithinDetectRange();
            navAgent.stoppingDistance = 2f;
            if (enemy.character.CurrentState is DuckingState)
            {
                Debug.Log("The character is ducking and cannot be detected");
            }
            if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                // Set a new random destination
                SetRandomDestination();
            }
        }
    }
}

