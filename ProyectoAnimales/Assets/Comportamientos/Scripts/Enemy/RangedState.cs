using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private EnemyAI enemy;

    public float timer;
    
    public void Execute()
    {
        enemy.ThrowKnife();
        if(enemy.Target != null)
        {
            enemy.Move();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }
    public void Enter(EnemyAI enemy)
    {
        this.enemy = enemy;
        enemy.estadoTxt.text = "Atacando";
    }
    public void Exit()
    {

    }
    public void MyOnTriggerEnter(Collider2D other)
    {
        if(other.tag == "Edge")
        {
            enemy.sight.enabled = false;
            //sumaTimer();
            enemy.ChangeDirection();
            enemy.ChangeState(new PatrolState());
        }
    }

    public void sumaTimer()
    {
        timer += Time.deltaTime;

        if(timer >= 1f)
        {
            enemy.sight.enabled = true;
            timer = 0f;
        }
    }
}
