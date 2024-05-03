using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct SpawnSequence
{
    [SerializeField] EnemyTypes[] _enemyTypes;
    Dictionary<EnemyTypes, EnemyModel> _enemies;
    public EnemyModel EnemyModel(EnemyTypes type) {  return _enemies[type]; }
}