using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SeekingState : EnemyState
    {
        public SeekingState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("The enemy was close enough and has detected the player.");
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (enemy.character.CurrentState is DuckingState) //if the character's currently in ducking state, the enemy cannot detect the player
            {
                Debug.Log("The character is ducking and cannot be detected");
                stateMachine.ChangeEnemyState(enemy.patrolState); //switch back to patrol
            }
        }
        public override void Exit()
        {
            base.Exit();
            Debug.Log("Exiting seeking state");
        }
    }
}