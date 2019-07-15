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
        private Joystick _joystick;
        private Joybutton _joybutton;

        public Player()
        {
            this.role = Role.Player;
            this.bulletTime = 0.4f;
        }

        internal override void Init()
        {
            currentDirection = Direction.Up;
            isDefend = true;
            defendTime = 3.0f;
            role = Role.Player;
            _joystick = FindObjectOfType<Joystick>();
            _joystick.AxisOptions = AxisOptions.Both;
            _joybutton = FindObjectOfType<Joybutton>();
        }

        protected override bool CanAttack()
        {
            return _joybutton.isPressed && bulletTime > 0.4f;
        }



        internal override void UpdateInternal()
        {
            updateVelocity();
            playAudio();
            bulletTime += Time.deltaTime;
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
            if (Mathf.Abs(velocity) > 0.0005f)
            {
                audioSource.clip = moveAudios[0];
            }
            else
            {
                audioSource.clip = moveAudios[1];
            }

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        private float getInputX()
        {
            float v = Input.GetAxisRaw("Vertical");
            if (Mathf.Abs(v) <= 0.005f)
            {
                if (Mathf.Abs(_joystick.Vertical) >= Mathf.Abs(_joystick.Horizontal))
                {
//                    v = _joystick.Vertical;
                    if (Mathf.Abs(_joystick.Vertical) >= 0.001)
                    {
                        v = 1.0f * (_joystick.Vertical > 0 ? 1 : -1);
                    }
                }
            }

            return v;
        }


        private float getInputY()
        {
            float v = Input.GetAxisRaw("Horizontal");
            if (Mathf.Abs(v) <= 0.005f)
            {
                if (Mathf.Abs(_joystick.Vertical) < Mathf.Abs(_joystick.Horizontal))
                {
//                    v = _joystick.Horizontal;
                    if (Mathf.Abs(_joystick.Horizontal) >= 0.001)
                    {
                        v = 1.0f * (_joystick.Horizontal > 0 ? 1 : -1);
                    }
                }
            }

            return v;
        }

        protected override void FixedUpdateInternal()
        {
        }

    }
}