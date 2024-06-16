using Panda.Examples.PlayTag;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SeekingState : EnemyState
    {
        //Transform target;
        NavMeshAgent navAgent;
        GameObject creature;
        CreatureTasks creatureTasks;
        public float rangeToDetectPlayer = 10f; //range where the enemy will be able to detect the player
        public SeekingState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
        bool PlayerWithinDetectRange() //boolean that returns true when the character is within specified distance of the enemy
        {
            float distance = Vector3.Distance(enemy.transform.position, enemy.player.transform.position); //calculate the distance ebtween the enemy and the player
            if (distance < rangeToDetectPlayer)
            {
                return true; 
            }
            else return false;
            //Debug.Log("Checking if player is within range " + distance);
        }
        void MoveToCharacter()
        {
            navAgent.stoppingDistance = 3f;

            if (navAgent.destination != enemy.player.transform.position)
            {
                navAgent.SetDestination(enemy.player.transform.position);
            }
            if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                stateMachine.ChangeEnemyState(enemy.attackingState);
            }
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("The enemy was close enough and has detected the player.");
            navAgent = enemy.navAgent;
            creature = enemy.creature;
            creatureTasks = creature.GetComponent<CreatureTasks>();
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!PlayerWithinDetectRange() || enemy.character.CurrentState is DuckingState) //if the character's currently in ducking state, the enemy cannot detect the player
            {
                //Debug.Log("The character is ducking and cannot be detected");
                stateMachine.ChangeEnemyState(enemy.patrolState); //switch back to patrol
            }
            else if (creatureTasks.tauntActionDone) //if the creature has taunted the enemy, it will switch to its scared state
            {
                stateMachine.ChangeEnemyState(enemy.scaredState);
            }
            else MoveToCharacter();
        }
        public override void Exit()
        {
            base.Exit();
            Debug.Log("Exiting seeking state");
        }
    }
}