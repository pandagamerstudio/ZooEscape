using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{

    private EnemyAI enemy;


    private float patrolTimer;
    private float patrolDuration = 10f;
    public void Execute()
    {
        Patrol();

        enemy.Move();

        if(enemy.Target != null)
        {
            enemy.ChangeState(new RangedState());
        }
    }
    public void Enter(EnemyAI enemy)
    {
        this.enemy = enemy;
    }
    public void Exit()
    {

    }
    public void OnTriggerEnter(Collider2D other)
    {
        if(other.tag == "Edge")
        {
            enemy.ChangeDirection();
        }
    }


    private void Patrol()
    {
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
