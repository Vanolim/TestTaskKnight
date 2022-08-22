using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _initialPosition;
    [SerializeField] private Transform _leftSpawnPosition;
    [SerializeField] private Transform _rightSpawnPosition;
    [SerializeField] private EnemyPrefab _enemyPrefab;

    public EnemyPrefab Spawn()
    {
        EnemyPrefab enemy = Instantiate(_enemyPrefab, _initialPosition);
        return enemy;
    }
}