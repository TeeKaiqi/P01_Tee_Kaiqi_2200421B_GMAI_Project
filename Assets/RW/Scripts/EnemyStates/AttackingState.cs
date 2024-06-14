using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class AttackingState : EnemyState
    {
        public AttackingState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}