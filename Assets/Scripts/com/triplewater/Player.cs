using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.triplewater
{


    public class Player : Tank
    {
        public Player()
        {
            this.role = Role.Player;
        }

        internal override void Init()
        {
            currentDirection = Direction.Up;
            isDefend = true;
            defendTime = 3.0f;
            role = Role.Player;
                
        }


        internal override void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GenerateBullet();
            }
        }

        internal override void UpdateInternal()
        {

            float v = Input.GetAxisRaw("Vertical");

            if (v < 0)
            {
                currentDirection = Direction.Down;

            }
            else if (v > 0)
            {
                currentDirection = Direction.Up;

            }

            if (v > 1e-6 || v < -1e-6)
            {
                velocity = Math.Abs(v);
                return;
            }

            float h = Input.GetAxisRaw("Horizontal");

            if (h < 0)
            {
                currentDirection = Direction.Left;
            }
            else if (h > 0)
            {
                currentDirection = Direction.Right;
            }

            velocity = Math.Abs(h);
        }

        protected override void FixedUpdateInternal()
        {

        }

    }
}
