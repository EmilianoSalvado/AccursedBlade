using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : EnemyWeapon
{
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _bulletPoint;
    public override void Attack()
    {
        Instantiate(_bulletPrefab, _bulletPoint.position, transform.rotation);
    }
}