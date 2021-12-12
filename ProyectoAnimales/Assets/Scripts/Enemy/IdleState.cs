using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{

    private EnemyAI enemy;


    private float idleTimer;
    private float idleDuration = 5f;
    public void Execute()
    {
        Idle();

        if(enemy.Target != null)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
    public void Enter(EnemyAI enemy)
    {
        this.enemy = enemy;
    }
    public void Exit()
    {

    }
    public void MyOnTriggerEnter(Collider2D other)
    {

    }

    private void Idle()
    {
        enemy.myAnimator.SetFloat("speed", 0);

        idleTimer += Time.deltaTime;

        if(idleTimer >= idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}

