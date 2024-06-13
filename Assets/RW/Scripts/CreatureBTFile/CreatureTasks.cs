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
        public float speed = 5f;

        private Rigidbody rb;
        private UnityEngine.AI.NavMeshAgent navAgent;
        private CreatureController creature;
        private Character character;

        Transform target;
        GameObject player;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            navAgent = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player");
            player = GameObject.FindGameObjectWithTag("Player");
            creature = GetComponent<CreatureController>();
            character = player.GetComponent<Character>();

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

