using UnityEngine;

public class EnemyHealthSystem : HealthSystem
{
    [SerializeField] Enemy _enemy;

    public override void GetDamage(float dmg)
    {
        base.GetDamage(dmg);
        _enemy.GetRepeled();
    }

    public override void OnDead()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}