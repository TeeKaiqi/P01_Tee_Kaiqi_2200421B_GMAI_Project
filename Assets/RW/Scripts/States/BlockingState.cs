using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class BlockingState : GroundedState
    {
        public BlockingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }
    }
}
