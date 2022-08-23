public class Services
{
    public GameReloader GameReloader { get; }
    public GameFinisher GameFinisher { get; }
    public IDisposableHandler DisposableHandler { get; }
    public PlayerInput PlayerInput { get; }

    public Services(SceneContext sceneContext)
    {
        PlayerInput = new PlayerInput();
        PlayerInput.Enable();
        
        GameReloader = new GameReloader(sceneContext.LoseView);
        GameFinisher = new GameFinisher(sceneContext.LoseView, sceneContext.StartView);
        DisposableHandler = new DisposableHandler();
    }
}