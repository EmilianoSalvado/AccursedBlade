using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct Wave
{
    [SerializeField] EnemyTypes[] _sequence;
    public EnemyTypes[] Sequence { get { return _sequence; } }
    [SerializeField] Transform[] _spawnPoints;
    public Transform[] SpawnPoints { get { return _spawnPoints; } }
}