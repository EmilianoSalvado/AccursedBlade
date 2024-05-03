using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] List<EnemyModel> _currentEnemies = new List<EnemyModel>();
    [SerializeField] EnemyModel _swordsmanPrefab, _gunnerPrefab;
    [SerializeField] Transform _playerTransform;
    [SerializeField] EnemiesIndex _index;
    Dictionary<EnemyTypes, EnemyModel> _prefabs = new Dictionary<EnemyTypes, EnemyModel>();

    public int EnemyCount { get { return _currentEnemies.Count; } }
    public static EnemyManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _prefabs.Add(EnemyTypes.Swordsman, _swordsmanPrefab);
        _prefabs.Add(EnemyTypes.Gunner, _gunnerPrefab);
    }

    public void Spawn(Vector3[] spawnPoints, EnemyTypes enType)
    {
        foreach (var sp in spawnPoints)
        {
            var en = Instantiate(_index.Index[enType], sp, Quaternion.identity);
            en.SetPlayer(_playerTransform);
            _currentEnemies.Add(en);
        }
    }

    public void GetKill(EnemyModel enKilled)
    {
        if (_currentEnemies.Contains(enKilled))
        {
            _currentEnemies.Remove(enKilled);
        }
    }
}