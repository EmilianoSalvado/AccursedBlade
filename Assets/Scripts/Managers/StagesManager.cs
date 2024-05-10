using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagesManager : MonoBehaviour
{
    [SerializeField] int[] _wavesPerStage;
    [SerializeField] EnemySpawn[] _spawners;
    [SerializeField] Platform[] _platforms;
    int  _stageCount = 0;
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

    IEnumerator StageRoutine()
    {
        if (_stageCount > 0)
        { _platforms[_stageCount - 1].PlatformOn(false); }

        for (int j = 0; j < _spawners[_stageCount].Waves.Length; j++)
        {
            yield return new WaitForSeconds(_waveDelay);

            _spawners[_stageCount].AuxTrigger();

            while (EnemyManager.Instance.EnemyCount > 0)
            { yield return new WaitForSeconds(.5f); }
        }

        if (_stageCount <= _platforms.Length - 1)
        {
            _platforms[_stageCount].PlatformOn(true);
        }

        _stageCount++;
    }

    public void SpawnerTriggered()
    {
        StartCoroutine(StageRoutine());
    }
}