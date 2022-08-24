public class Game
{
    private readonly GameUpdate _gameUpdate;
    private readonly EnemyCollection _enemyCollection;
    private readonly SceneContext _sceneContext;
    private readonly Hero _hero;
    private readonly Player _player;
    private readonly Services _services;
    private readonly HealthSpawner _healthSpawner;
    private readonly GameDataPreset _gameDataPreset;

    public Game(GameUpdate gameUpdate, SceneContext sceneContext, HeroSpawner heroSpawner, EnemySpawner enemySpawner, HealthSpawner healthSpawner)
    {
        _gameUpdate = gameUpdate;
        _sceneContext = sceneContext;
        _services = new Services(_sceneContext);
        _hero = new Hero(heroSpawner.Spawn(), new Health(), _services.PlayerInput, _sceneContext.HealthView, _gameDataPreset.HeroDataPreset);
        _enemyCollection = new EnemyCollection(_hero.Transform, enemySpawner, _gameDataPreset.EnemyCollectionDataPreset);
        _enemyCollection.OnEnemyKilled += SpawnHeart;
        _player = new Player(_hero, _enemyCollection, _sceneContext.CoreView, _sceneContext.LoseView);
        _healthSpawner = healthSpawner;
        
        Init();
    }

    private void Init()
    {
        InitView();
        InitPlayer();
        Register();
    }

    private void InitView()
    {
        _sceneContext.StartView.Activate();
        _sceneContext.StartView.OnStart += Start;
    }


    private void InitPlayer() => _player.PlayerLose.OnLose += DeactivateObjects;

    private void Start()
    {
        _services.PlayerInput.Enable();
        _sceneContext.StartView.Deactivate();
        _hero.Activate();
        _enemyCollection.Start();
    }

    private void DeactivateObjects()
    {
        _player.PlayerLose.OnLose -= DeactivateObjects;
        _enemyCollection.Deactivate();
        _healthSpawner.Deactivate();
        _services.PlayerInput.Disable();
    }

    private void SpawnHeart(Enemy killedEnemy) => _healthSpawner.Spawn(killedEnemy.EnemyPrefab.Center.position);

    private void Register()
    {
        _services.DisposableHandler.Register(_player.Core);
        _services.DisposableHandler.Register(_player.PlayerLose);
        _services.DisposableHandler.Register(_hero);

        _gameUpdate.Register(_hero);
        _gameUpdate.Register(_enemyCollection);
    }
}