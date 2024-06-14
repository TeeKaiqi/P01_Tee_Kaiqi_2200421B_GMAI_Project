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
        private float speed = .5f;
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
            navAgent.speed = speed;
            player = GameObject.FindGameObjectWithTag("Player");
            character = player.GetComponent<Character>();
            creatureTasks = GetComponent<CreatureTasks>();
            anim = GetComponent<Animator>();

            movementSM = new StateMachine();
            patrolState = new PatrollingState(this, movementSM);
            seekingState = new SeekingState(this, movementSM);

            movementSM.IntialiseEnemy(patrolState);
        }

        public void UpdateAnimationParameters() //method that accesses the animator parameters so that it can update the movement and make the blend tree work
        {
            Vector3 velocity = navAgent.velocity; //set the velocity as the velocity from the navagent
            Vector3 localVelocity = transform.InverseTransformDirection(velocity); //referenced from ChatGPT
            //I'm using localVelocity instead of velocity because that way the movement parameters align with the object's orientation
            //This way when the slime turns, the animation will point in the correct direction compared to if it used normal world space velocity

            // Set the Animator parameters
            anim.SetFloat(horizonalMoveParam, localVelocity.x);
            anim.SetFloat(verticalMoveParam, localVelocity.z);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimationParameters();
            movementSM.CurrentEnemyState.HandleInput();
            movementSM.CurrentEnemyState.LogicUpdate();
        }
    }
}

