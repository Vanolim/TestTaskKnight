using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private GameUpdate _gameUpdate;
    [SerializeField] private HeroSpawner _heroSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private SceneContext _sceneContext;
    [SerializeField] private HealthSpawner _healthSpawner;

    private Game _game;

    private void Start()
    {
        _game = new Game(_gameUpdate, _sceneContext, _heroSpawner, _enemySpawner, _healthSpawner);
    }
}