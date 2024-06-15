using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class SwingWeaponState : GroundedState
    {
        private bool swingAnimationDone; //boolean that keeps track of the swing animation
        private int swingMelee => Animator.StringToHash("SwingMelee"); //access the animator by storing the name of the parameter as swingMelee
        private float attackRange = 2f;
        private float attackAnimationTime = 1f;
        public void Start()
        {
            swingAnimationDone = false; //set the variable to false
        }

        public SwingWeaponState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        private IEnumerator SwingCoroutine() //coroutine that waits for the the animation to finish 
        {
            yield return new WaitForSeconds(attackAnimationTime);

            Collider[] hitColliders = Physics.OverlapSphere(character.transform.position, attackRange);
            foreach (Collider collider in hitColliders)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage();
                }
                else
                {
                    //Debug.Log("unable to find enemy collider");
                }

            }
            swingAnimationDone = true;
        }
        public override void Enter()
        {
            base.Enter();
            //Debug.Log("SwingWeaponState entered.");
            character.TriggerAnimation(swingMelee); //trigger the swing animation
            swingAnimationDone = false; //set the animation bool to true
            character.StartCoroutine(SwingCoroutine());
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (swingAnimationDone) //when the bool is true, following logic ensues 
            {
                //Debug.Log("SwingState Logic reached.");
                stateMachine.ChangeState(character.standing); //change to standing state 
            }
        }

        public override void Exit()
        {
            base.Exit();
            //Debug.Log("SwingState exit logic reached.");
        }
    }
}