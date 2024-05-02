using System.Linq;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Transform[] _pointsTransform;
    [SerializeField] EnemyTypes _enemyTypes;

    private void OnTriggerEnter(Collider other)
    {
        StagesManager.Instance.SpawnerTriggered();
        GetComponent<Collider>().enabled = false;
    }

    public void AuxTrigger()
    {
        EnemyManager.Instance.Spawn(_pointsTransform.Select(x => x.position).ToArray(), _enemyTypes);
    }
}
