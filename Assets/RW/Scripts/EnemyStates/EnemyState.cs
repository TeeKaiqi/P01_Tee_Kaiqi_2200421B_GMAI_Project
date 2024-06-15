using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public abstract class EnemyState
    { 
        protected Enemy enemy;
        protected StateMachine stateMachine;
        protected EnemyState(Enemy enemy,  StateMachine stateMachine )
        {
            this.enemy = enemy;
            this.stateMachine = stateMachine; 
        }

        public virtual void Enter() { }
        public virtual void LogicUpdate() { }
        public virtual void PhysicsUpdate() { }
        public virtual void Exit() { }

    }
}

