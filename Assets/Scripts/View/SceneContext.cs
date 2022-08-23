using UnityEngine;

public class SceneContext : MonoBehaviour
{
    [SerializeField] private HealthView _healthView;
    [SerializeField] private CoreView _coreView;
    [SerializeField] private LoseView _loseView;
    [SerializeField] private StartView _startView;

    public HealthView HealthView => _healthView;
    public CoreView CoreView => _coreView;
    public LoseView LoseView => _loseView;
    public StartView StartView => _startView;
}
