using System;
using UnityEngine;
using Random = System.Random;

namespace com.triplewater
{


    public class Enemy : Tank
    {
        private float _runningTime;

        public Enemy()
        {
            role = Role.Enemy;
        }

        internal override void Init()
        {
            currentDirection = Direction.Down;
            _runningTime = GetRunningTime();
            velocity = 1.0f;
            bulletTime = 0.4f;
            role = Role.Enemy;
        }

        internal override void Attack()
        {
            if (CanAttack())
            {
                GenerateBullet();
                bulletTime = 0;
            }
        }

        protected override void FixedUpdateInternal()
        {
        }

        internal override void UpdateInternal()
        {
            UpdateDirection();
            _runningTime -= Time.deltaTime;
            bulletTime += Time.deltaTime;
        }

        bool CanAttack()
        {
            bool attack = new Random().NextDouble() > 0.5;
            return bulletTime > 0.4 && attack;
        }

        float GetRunningTime()
        {
            return (float) (Math.Abs(new Random().NextDouble()) * 5);
        }

        private void UpdateDirection()
        {
            if (_runningTime > 0)
            {
                return;
            }

            int direct = new Random().Next(0, 4);
            currentDirection = (Direction) direct;
            _runningTime = GetRunningTime();
        }
        
    }
}
