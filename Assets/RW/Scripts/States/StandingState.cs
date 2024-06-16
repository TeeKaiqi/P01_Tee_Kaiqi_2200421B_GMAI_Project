/*
 * Copyright (c) 2019 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using UnityEngine.UIElements;

namespace RayWenderlich.Unity.StatePatternInUnity
{
    public class StandingState : GroundedState
    {
        private bool jump;
        private bool crouch;
        private bool draw;
        private bool sheath;
        private bool swing;
        private bool dead;
        private bool block;

        public StandingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {

        }
        public override void Enter()
        {
            base.Enter();
            speed = character.MovementSpeed;
            rotationSpeed = character.RotationSpeed;
            crouch = false;
            jump = false;
            draw = false;
            sheath = false; 
            dead = false;
            block = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            crouch = Input.GetButtonDown("Fire3");
            jump = Input.GetButtonDown("Jump");
            draw = Input.GetKey(KeyCode.F);
            sheath = Input.GetKeyDown(KeyCode.G);
            swing = Input.GetMouseButtonDown(0);
            block = Input.GetKeyDown(KeyCode.E);
            //dead = Input.GetKeyDown(KeyCode.P);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (crouch)
            {
                stateMachine.ChangeState(character.ducking);
            }
            else if (jump)
            {
                stateMachine.ChangeState(character.jumping);
            }
            else if (!character.isWeaponOut && draw) //checks to make sure that the character doesn't already have a weapon out and that the user pressed f
            {
                stateMachine.ChangeState(character.drawWeapon); //change the state
            }
            else if (character.isWeaponOut && sheath) //check that the weapon is out and the player pressed g before changing state
            {
                stateMachine.ChangeState(character.sheathWeapon);
            }
            else if (character.isWeaponOut && swing) //check that the weapon is out and that the player pressed left mouse button
            {
                stateMachine.ChangeState(character.swingWeapon);
            }
            else if (block)
            {
                stateMachine.ChangeState(character.block);
            }
            //else if (dead)
            //{
            //    stateMachine.ChangeState(character.death);
            //}
        }

    }
}
