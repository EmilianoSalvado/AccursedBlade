using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagesManager : MonoBehaviour
{
    [SerializeField] int[] _wavesPerStage;
    int _stages { get { return _wavesPerStage.Length; } }
    [SerializeField] EnemySpawn[] _spawners;
    [SerializeField] PlatformAvailability[] _platforms;
    int _waveCount = 0, _stageCount = 0;
    [SerializeField] float _waveDelay;
    [SerializeField] bool _spawnTriggered;
    public static StagesManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
    }

    public void Start()
    {
        StartCoroutine(StageRoutine());
    }

    IEnumerator StageRoutine()
    {
        while (!_spawnTriggered)
        {
            yield return new WaitForSeconds(.5f);
        }

        for (int i = _stageCount; i < _wavesPerStage.Length; i++)
        {
            _stageCount = i;
            _spawnTriggered = false;

            if (_stageCount > 0)
            { _platforms[_stageCount - 1].PlatformOff(); }

            for (int j = _waveCount; j < _wavesPerStage[_waveCount]; j++)
            {
                _waveCount = j;
                yield return new WaitForSeconds(_waveDelay);

                foreach (var spawner in _spawners[i].GetComponents<EnemySpawn>())
                { spawner.AuxTrigger(); }

                while (EnemyManager.Instance.EnemyCount > 0)
                { yield return new WaitForSeconds(.5f); }
            }

            if (_stageCount <= _platforms.Length - 1)
            {
                _platforms[_stageCount].PlatformOn();
            }

            while (!_spawnTriggered)
            {
                yield return new WaitForSeconds(.5f);
            }
        }
    }

    public void SpawnerTriggered()
    {
        _spawnTriggered = true;
    }
}
