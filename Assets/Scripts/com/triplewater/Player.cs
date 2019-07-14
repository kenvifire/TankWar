using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;


namespace com.triplewater
{


    public class Player : Tank
    {
        public AudioClip[] moveAudios;
        protected Joystick joystick;
        protected Joybutton Joybutton;
        

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
            joystick = FindObjectOfType<Joystick>();
            Joybutton = FindObjectOfType<Joybutton>();

        }


        internal override void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Joybutton.isPressed)
            {
                GenerateBullet();
            }
        }

        internal override void UpdateInternal()
        {
            updateVelocity();
            playAudio();
        }

        private void updateVelocity()
        {

            float v = getInputX();

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

            float h = getInputY();

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

        private void playAudio()
        {
            if (Mathf.Abs(velocity) > 0.05f)
            {
                audioSource.clip = moveAudios[0];
            }
            else
            {
                audioSource.clip = moveAudios[1];
            }

            if (!audioSource.isPlaying)
            {
            }
            
        }

        private float getInputX()
        {
            float v = Input.GetAxisRaw("Vertical");
            if (Mathf.Abs(v) <= 0.005f)
            {
                if (Mathf.Abs(joystick.Vertical) >= Mathf.Abs(joystick.Horizontal))
                {
                    v = joystick.Vertical;
                }
            }

            return v;

        }
        

        private float getInputY()
        {
            float v = Input.GetAxisRaw("Horizontal");
            if (Mathf.Abs(v) <= 0.005f)
            {
                if (Mathf.Abs(joystick.Vertical) < Mathf.Abs(joystick.Horizontal))
                {
                    v = joystick.Horizontal;
                }
            }

            return v;  
        }

        protected override void FixedUpdateInternal()
        {

        }

    }
}
