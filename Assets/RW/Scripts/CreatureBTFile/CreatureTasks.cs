using Panda;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class CreatureTasks : MonoBehaviour
    {
        private Rigidbody rb;
        private NavMeshAgent navAgent;
        private CreatureController creature;
        private Character character;
        private Enemy enemyScript;

        Transform target;
        GameObject player;
        GameObject enemyCharacter;

        Animator animator;
        private int horizonalMoveParam = Animator.StringToHash("H_Speed");
        private int verticalMoveParam = Animator.StringToHash("V_Speed");

        public int heal => Animator.StringToHash("Heal"); //access to the parameter in the animator
        public int taunt => Animator.StringToHash("Taunt");
        public int victory => Animator.StringToHash("Victory");

        public bool tauntActionDone;
        public bool celebratedAlready;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
            creature = GetComponent<CreatureController>();
            navAgent.speed = creature.speed;

            rb = GetComponent<Rigidbody>(); 
            player = GameObject.FindGameObjectWithTag("Player");
            enemyCharacter = GameObject.FindGameObjectWithTag("Enemy");
            character = player.GetComponent<Character>();
            enemyScript = enemyCharacter.GetComponent<Enemy>();

            tauntActionDone = false; 
            celebratedAlready = false;
        }

        private void Update()
        {
            UpdateAnimationParameters();
        }

        public void UpdateAnimationParameters() //method that accesses the animator parameters so that it can update the movement and make the blend tree work
        {
            Vector3 velocity = navAgent.velocity; //set the velocity as the velocity from the navagent
            Vector3 localVelocity = transform.InverseTransformDirection(velocity); //referenced from ChatGPT
            //I'm using localVelocity instead of velocity because that way the movement parameters align with the object's orientation
            //This way when the slime turns, the animation will point in the correct direction compared to if it used normal world space velocity

            // Set the Animator parameters
            animator.SetFloat(horizonalMoveParam, localVelocity.x);
            animator.SetFloat(verticalMoveParam, localVelocity.z);
        }

        [Task]
        void DistanceBetweenPlayer()
        {
            //Debug.Log("Checking distance between player");
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < creature.healingDistance)
            {
                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail();
            }
        }

        [Task]
        void CheckHealth()
        {
            //Debug.Log("Character's health is " + character.playerHealth);
            if (character.playerHealth < 3 && character.playerHealth > 0)
            {
                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail();
            }
        }

        [Task]
        void Heal()
        {
            Debug.Log("Healing");
            animator.SetTrigger(heal);
            character.playerHealth += 1;
            Task.current.Succeed();
        }

        [Task]
        void CheckIfDead()
        {
            if (enemyScript.movementSM.CurrentEnemyState is DeadState)
            {
                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail();
            }
        }

        [Task]
        void Celebrate()
        {
            if (!celebratedAlready)
            {
                celebratedAlready = true;
                animator.SetTrigger(victory);
                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail();
            }
        }

        [Task]
        void EnemyIsClose()
        {
            float distance = Vector3.Distance(transform.position, enemyCharacter.transform.position);
            if (distance < creature.tauntingDistance)
            {
                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail();
            }
        }

        [Task]
        bool AlreadyTaunted()
        {
            return (!tauntActionDone);
        }

        [Task]
        void EnemyState()
        {
            if (enemyScript.movementSM.CurrentEnemyState is PatrollingState || enemyScript.movementSM.CurrentEnemyState is SeekingState)
            {
                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail();
            }
        }
        [Task]
        void Taunt()
        {
            Debug.Log("Taunting");
            tauntActionDone = true; //set the actiondone to true so that the enemy can know to change to scaredstate
            animator.SetTrigger(taunt);
            Task.current.Succeed();
        }

        [Task]
        void MoveToPlayer()
        {
            navAgent.stoppingDistance = 4f;

            if (navAgent.destination != player.transform.position)
            {
                navAgent.SetDestination(player.transform.position);
            }
            if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                Task.current.Succeed();
                target = null;
            }
        }
    }

}

