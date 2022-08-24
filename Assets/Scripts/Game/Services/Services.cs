public class Services
{
    private readonly GameReloader _gameReloader;
    private readonly GameFinisher _gameFinisher;
    public IDisposableHandler DisposableHandler { get; }
    public PlayerInput PlayerInput { get; }

    public Services(SceneContext sceneContext)
    {
        PlayerInput = new PlayerInput();
        PlayerInput.Disable();

        DisposableHandler = new DisposableHandler();
        _gameReloader = new GameReloader(sceneContext.LoseView);
        _gameFinisher = new GameFinisher(sceneContext.LoseView, sceneContext.StartView, DisposableHandler);

        Register();
    }

    private void Register()
    {
        DisposableHandler.Register(_gameReloader);
        DisposableHandler.Register(_gameFinisher);
    }
}