using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class AttackingState : EnemyState
    {
        Animator animator;
        private float attackRadius = 3f; //radius the creature has to be in to attack the player
        private float attackAnimationTime = 1.75f; //rough time it takes for the attack animation to play
        private bool attackAnimationDone; //bool that keeps track of it the animation has finished playing
        private int attack = Animator.StringToHash("Attack");

        public AttackingState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        private IEnumerator AttackCoroutine() //coroutine that checks for collisions in a sphere around the enemy
        {
            yield return new WaitForSeconds(attackAnimationTime); //wait for the specified animation time

            Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, attackRadius); //list that stores the colliders in sphere around enemy's postion
            foreach (Collider collider in hitColliders)
            {
                Character character = collider.GetComponent<Character>(); //tries to get character component from the game object 
                if (character != null && character.CurrentState is not BlockingState) //if the gameobject has charcater attached and is not blocking
                {
                    character.TakeDamage(); //character takes damage
                }
                else
                {
                    //Debug.Log("no character collider found or character blocked");
                }
            }
            attackAnimationDone = true; //set the bool to true
        }
        // Start is called before the first frame update
        public override void Enter()
        {
            base.Enter();
            Debug.Log("AttackState entered");
            animator = enemy.anim;
            attackAnimationDone = false;
            animator.SetTrigger(attack);
            enemy.StartCoroutine(AttackCoroutine()); //start the coroutine

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