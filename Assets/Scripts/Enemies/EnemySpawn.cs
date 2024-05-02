using System.Linq;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Transform[] _pointsTransform;
    [SerializeField] EnemyTypes _enemyTypes;

    private void OnTriggerEnter(Collider other)
    {
        EnemyManager.Instance.Spawn(_pointsTransform.Select(x => x.position).ToArray(), _enemyTypes);
    }
}
