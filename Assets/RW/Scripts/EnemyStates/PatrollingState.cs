using Panda.Examples.PlayTag;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class PatrollingState : EnemyState
    {
        public NavMeshAgent navAgent;
        public float range = 50f; //set range of the sphere that the enemy can patrol around
        public float rangeToDetectPlayer = 10f; //range where the enemy will be able to detect the player
        public PatrollingState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        bool RandomPoint(Vector3 center, float range, out Vector3 result) //referenced from https://youtu.be/dYs0WRzzoRc
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range; 
            //randomPoint is a vector3 that generates a random point within the sphere of range variable that is passed in 
            //It is added to the centre so that it shifts this point to be around the centre(Which will be the position of the enemy)
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas)) //Checks if the randomPoint given is on the navMesh
                //The 1f that is passed in specifies the maximum distance to be checked when checking if the point is on the navMesh
            {
                result = hit.position; //stores the result position of the check in the result variable
                return true; //returns true if the random point was found on naMesh
            }
            result = Vector3.zero; //if the random point wasn't found, set the result as zero and return false
            return false;
        }

        void SetRandomDestination()
        {
            Vector3 randomDestination;
            if (RandomPoint(enemy.transform.position, range, out randomDestination)) //If RandomPoint returns true (which means the randomPoint is actually a point on the navMesh)
                //store the Vector3 result in randomDestination
            {
                navAgent.SetDestination(randomDestination); //set the desination of the navAgent as the randomDestination
            }
        }

        bool PlayerWithinDetectRange() //boolean that returns true when the character is within specified distance of the enemy
        {
            float distance = Vector3.Distance(enemy.transform.position, enemy.player.transform.position); //calculate the distance ebtween the enemy and the player
            if (distance < rangeToDetectPlayer)
            {
                return true; //return true if 5f is greater than the distance
            }
            else return false;
            //Debug.Log("Checking if player is within range " + distance);
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Enemy has entered its Patrol state");
            navAgent = enemy.navAgent;
            SetRandomDestination(); //call the function in the beginning so the enemy starts moving almost immediately
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            navAgent.stoppingDistance = 2f;

            if (PlayerWithinDetectRange() && enemy.character.CurrentState is not DuckingState) //if the bool is true
            {
                stateMachine.ChangeEnemyState(enemy.seekingState); //change state
            }

            if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance) 
                //if the navAgent is no longer calculating the path to its destination and the remaining distance is less than specified
            { 
                SetRandomDestination(); //then set a new randomdestination
            }
        }
    }
}

