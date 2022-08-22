public class Game
{
    private readonly GameUpdate _gameUpdate;
    private PlayerInput _playerInput;
    private EnemyCollection _enemyCollection;
    private HeroPrefab _heroPrefab;
    
    public Game(GameUpdate gameUpdate, HeroSpawner heroSpawner)
    {
        _gameUpdate = gameUpdate;
        
        Init(heroSpawner);
    }

    private void Init(HeroSpawner heroSpawner)
    {
        InitInput();
        InitHero(heroSpawner);
        InitEnemyCollection();
    }

    private void InitInput()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    private void InitHero(HeroSpawner heroSpawner)
    {
        _heroPrefab = heroSpawner.Spawn();
        ICharacterStates heroStates = new HeroStates();
        InputHandler inputHandler = new InputHandler(_playerInput, heroStates);
        
        Hero hero = new Hero(_heroPrefab, heroStates, inputHandler, new Health());

        HeroBoundHandler heroBoundHandler = new HeroBoundHandler(inputHandler, _heroPrefab.RB, heroStates);
        IDetectorFinishAnimation detectorFinishAnimation = new DetectorFinishAnimation(hero.CharacterAnimator);
        StateResetter stateResetter = new StateResetter(detectorFinishAnimation, heroStates);
        
        heroStates.SetInitialState();
        _gameUpdate.Register(detectorFinishAnimation);
        _gameUpdate.Register(hero);
        _gameUpdate.Register(heroBoundHandler);
    }

    private void InitEnemyCollection()
    {
        EnemyCollection enemyCollection = new EnemyCollection(_heroPrefab.transform);
        enemyCollection.Init();
    }
}