using Panda.Examples.PlayTag;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SeekingState : EnemyState
    {
        //Transform target;
        NavMeshAgent navAgent;
        GameObject creature;
        CreatureTasks creatureTasks;

        public SeekingState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        bool SeekPlayer()
        {
            navAgent.stoppingDistance = 1f;

            if (navAgent.destination != enemy.player.transform.position)
            {
                navAgent.SetDestination(enemy.player.transform.position);
            }

            if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                return true;
            }
            else return false;
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
            if (enemy.character.CurrentState is DuckingState) //if the character's currently in ducking state, the enemy cannot detect the player
            {
                Debug.Log("The character is ducking and cannot be detected");
                stateMachine.ChangeEnemyState(enemy.patrolState); //switch back to patrol
            }
            else if (creatureTasks.tauntActionDone) //if the creature has taunted the enemy, it will switch to its scared state
            {
                stateMachine.ChangeEnemyState(enemy.scaredState);
            }
        }
        public override void Exit()
        {
            base.Exit();
            Debug.Log("Exiting seeking state");
        }
    }
}