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

    public Vector2 GetSpawnPosition()
    {
        int countSpawnPosition = 2;
        if (Random.Range(0, countSpawnPosition) == 0)
            return _leftSpawnPosition.position;
        
        return _rightSpawnPosition.position;
    }
}