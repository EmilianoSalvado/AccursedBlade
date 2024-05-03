using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "enemiesIndex", menuName = "ScriptableObjects/EnemiesIndex", order = 1)]
public class EnemiesIndex : ScriptableObject
{
    [SerializeField] EnemyTypes[] _enemyTypes;
    [SerializeField] EnemyModel[] _enemyPrefabs;
    Dictionary<EnemyTypes, EnemyModel> _index;
    public Dictionary<EnemyTypes, EnemyModel> Index {  get { return _index; } }

    private void OnEnable()
    {

        if (_enemyTypes.Length != _enemyPrefabs.Length)
        {
            throw new System.InvalidOperationException("Amount of types doesn't match amount of prefabs.");
        }

        _index = new Dictionary<EnemyTypes, EnemyModel>();

        for (int i = 0; i < _enemyTypes.Length; i++)
        {
            _index.Add(_enemyTypes[i], _enemyPrefabs[i]);
        }
    }
}