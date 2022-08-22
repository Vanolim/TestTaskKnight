public class Game
{
    private readonly GameUpdate _gameUpdate;
    private PlayerInput _playerInput;
    
    public Game(GameUpdate gameUpdate, HeroSpawner heroSpawner)
    {
        _gameUpdate = gameUpdate;
        
        Init(heroSpawner);
    }

    private void Init(HeroSpawner heroSpawner)
    {
        InitInput();
        InitHero(heroSpawner);
    }

    private void InitInput()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    private void InitHero(HeroSpawner heroSpawner)
    {
        HeroPrefab heroPrefab = heroSpawner.Spawn();
        IHeroStates heroStates = new HeroStates();
        InputHandler inputHandler = new InputHandler(_playerInput, heroStates);
        
        Hero hero = new Hero(heroPrefab, heroStates, inputHandler, new Health());

        HeroBoundHandler heroBoundHandler = new HeroBoundHandler(inputHandler, heroPrefab.RB, heroStates);
        DetectorFinishAnimation detectorFinishAnimation = new DetectorFinishAnimation(hero.HeroAnimator);
        StateResetter stateResetter = new StateResetter(detectorFinishAnimation, heroStates);
        
        heroStates.SetInitialState();
        _gameUpdate.Register(detectorFinishAnimation);
        _gameUpdate.Register(hero);
        _gameUpdate.Register(heroBoundHandler);
    }
}