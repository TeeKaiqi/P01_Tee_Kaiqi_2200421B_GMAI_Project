using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class AttackingState : EnemyState
    {
        Animator animator;
        private float attackRadius = 3f;
        private float attackAnimationTime = 1.75f;
        private bool attackAnimationDone;
        private int attack = Animator.StringToHash("Attack");

        public AttackingState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        private IEnumerator AttackCoroutine()
        {
            yield return new WaitForSeconds(attackAnimationTime);

            Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, attackRadius);
            foreach (Collider collider in hitColliders)
            {
                Character character = collider.GetComponent<Character>();
                if (character != null && character.CurrentState is not BlockingState)
                {
                    character.TakeDamage();
                }
                else
                {
                    Debug.Log("no character collider found or character blocked");
                }
            }
            attackAnimationDone = true;
        }
        // Start is called before the first frame update
        public override void Enter()
        {
            base.Enter();
            Debug.Log("AttackState entered");
            animator = enemy.anim;
            attackAnimationDone = false;
            animator.SetTrigger(attack);
            enemy.StartCoroutine(AttackCoroutine());

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (attackAnimationDone)
            {
                stateMachine.ChangeEnemyState(enemy.patrolState);
            }
            if (enemy.character.playerHealth <= 0)
            {
                stateMachine.ChangeEnemyState(enemy.patrolState);
            }
        }
        public override void Exit() 
        {
            base.Exit();
        }
    }
}