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
        public float speed = 3f;

        private Rigidbody rb;
        private UnityEngine.AI.NavMeshAgent navAgent;
        private CreatureController creature;
        private Character character;

        Transform target;
        GameObject player;

        Animator animator;
        private int horizonalMoveParam = Animator.StringToHash("H_Speed");
        private int verticalMoveParam = Animator.StringToHash("V_Speed");

        public int heal => Animator.StringToHash("Heal"); //access to the death parameter in the animator
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            navAgent = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player");
            player = GameObject.FindGameObjectWithTag("Player");
            creature = GetComponent<CreatureController>();
            character = player.GetComponent<Character>();
            animator = GetComponent<Animator>();
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
            Debug.Log("Checking distance between player");
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
            Debug.Log("Character's health is " + character.playerHealth);
            if (character.playerHealth < 3)
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
        void MoveToPlayer()
        {
            navAgent.stoppingDistance = 2f;

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

