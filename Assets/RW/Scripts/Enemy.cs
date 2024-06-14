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

        public GameObject player;
        public Character character;
        public CreatureTasks creatureTasks;
        public NavMeshAgent navAgent;
        
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
            navAgent = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Player");
            character = player.GetComponent<Character>();
            creatureTasks = GetComponent<CreatureTasks>();
            anim = GetComponent<Animator>();

            movementSM = new StateMachine();
            patrolState = new PatrollingState(this, movementSM);
            seekingState = new SeekingState(this, movementSM);

            movementSM.IntialiseEnemy(patrolState);
        }

        // Update is called once per frame
        void Update()
        {
            movementSM.CurrentEnemyState.HandleInput();
            movementSM.CurrentEnemyState.LogicUpdate();
        }
    }
}

