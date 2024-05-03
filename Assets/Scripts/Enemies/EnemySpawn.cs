using System.Linq;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Wave[] _waves;
    public Wave[] Waves { get { return _waves; } }
    int _count = 0;

    private void OnTriggerEnter(Collider other)
    {
        StagesManager.Instance.SpawnerTriggered();
        GetComponent<Collider>().enabled = false;
    }

    public void AuxTrigger()
    {
        int c = 0;
        foreach (var enType in _waves[_count].Sequence)
        {
            EnemyManager.Instance.Spawn(_waves[_count].SpawnPoints[c].position, enType);
            c++;
        }
        _count++;
    }
}