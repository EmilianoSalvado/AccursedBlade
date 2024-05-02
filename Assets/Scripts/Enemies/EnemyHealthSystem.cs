using System;
using UnityEngine;

public class EnemyHealthSystem : HealthSystem
{
    [SerializeField] EnemyModel _enemy;

    public override void OnDead()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        _enemy.OnDead();
        Debug.Log("i'm dead");
    }
}