using UnityEngine;

public class Game
{
    private readonly GameUpdate _gameUpdate;
    private PlayerInput _playerInput;
    private EnemyCollection _enemyCollection;
    private Hero _hero;
    private Player _player;
    
    public Game(GameUpdate gameUpdate, HeroSpawner heroSpawner)
    {
        _gameUpdate = gameUpdate;
        
        Init(heroSpawner);
    }

    public void Start()
    {
        _enemyCollection.Start();
    }

    private void Init(HeroSpawner heroSpawner)
    {
        InitInput();
        InitHero(heroSpawner);
        InitEnemyCollection();
        InitPlayer();
    }

    private void InitInput()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    private void InitHero(HeroSpawner heroSpawner)
    {
        HeroPrefab heroPrefab = heroSpawner.Spawn();

        _hero = new Hero(heroPrefab, new Health(), _playerInput);
        
        _gameUpdate.Register(_hero);
    }

    private void InitEnemyCollection()
    {
        _enemyCollection = new EnemyCollection(_hero.Transform);
        _gameUpdate.Register(_enemyCollection);
    }

    private void InitPlayer()
    {
        _player = new Player(_hero, _enemyCollection, FindCoreView());
    }

    private CoreView FindCoreView() => Object.FindObjectOfType<CoreView>();
}