using Panda.Examples.PlayTag;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class Enemy : MonoBehaviour
    {
        public float speed = 3f;
        public int enemyHealth = 2;

        GameObject player;
        private Character character;
        private CreatureTasks creatureTasks;
        private Rigidbody rb;
        private UnityEngine.AI.NavMeshAgent navAgent;
        
        
        private int horizonalMoveParam = Animator.StringToHash("H_Speed");
        private int verticalMoveParam = Animator.StringToHash("V_Speed");

        public Animator anim;
        public StateMachine movementSM;
        public PatrollingState patrolState;
        public ScaredState scaredState;
        public AttackingState attackingState;
        public DeadState deadState;
        public SeekingState seekingState;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            navAgent = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player");
            player = GameObject.FindGameObjectWithTag("Player");
            creatureTasks = GetComponent<CreatureTasks>();
            character = player.GetComponent<Character>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

