using System;
using UnityEngine;
using Random = System.Random;

public class Enemy: Tank
{
    public float moveSpeed = 3f;

    private float bulletTime = 0.4f;

    private float runningTime;

    private Direction currentDirection;
    
    public Enemy()
    {
        currentDirection = Direction.Down;
        runningTime = GetRunnigTime();
    }

    public override void Attack()
    {
        if (CanAttack())
        {
            GenerateBullet(GetBulletDirection());
            bulletTime = 0;
        }
    }

    void Move()
    {
        Direction direction = GetDirection();
        if (direction != currentDirection)
        {
            currentDirection = direction;
            runningTime = GetRunnigTime();
        }
        runningTime -= Time.fixedDeltaTime;
        base.Move(currentDirection, moveSpeed, Time.fixedDeltaTime);

    }


    void Die()
    {
        base.Die();
    }

    internal override void FixedUpdateInternal()
    {
        Move();
    }

    internal override void UpdateInternal()
    {
        bulletTime += Time.deltaTime;
        Attack();
    }

    bool CanAttack()
    {
        bool attack = new Random().NextDouble() > 0.5;
        return bulletTime > 0.4 && attack;
    }

    Vector3 GetBulletDirection()
    {
        float z = 0;

        switch (currentDirection)
        {
            case Direction.Up:
                z = 0;
                break;
            case Direction.Down:
                z = -180;
                break;
            case Direction.Left:
                z = 90;
                break;
            case Direction.Right:
                z = -90;
                break;
        }
        return new Vector3(0, 0, z);
    }

    float GetRunnigTime()
    {
        return (float)(Math.Abs(new Random().NextDouble()) * 5);
    }

    Direction GetDirection()
    {
        if (runningTime > 0)
        {
            return currentDirection;
        }
        int direct = new Random().Next(0, 4);
        Direction direction = (Direction) direct;
        return direction;
    }
}
