using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Execute();
    void Enter(EnemyAI enemy);
    void Exit();
    void MyOnTriggerEnter(Collider2D other);
}
