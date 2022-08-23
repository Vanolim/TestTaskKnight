using System;
using UnityEngine;
using UnityEngine.UI;

public class LoseView : MonoBehaviour
{
    [SerializeField] private Button _restart;
    [SerializeField] private Button _exit;
    
    public event Action OnRestart;
    public event Action OnExit;

    public void Activate() => gameObject.SetActive(true);
    
    private void OnEnable()
    {
        _restart.onClick.AddListener(delegate { OnRestart?.Invoke(); });
        _exit.onClick.AddListener(delegate { OnExit?.Invoke(); });
    }

    private void OnDisable()
    {
        _restart.onClick.RemoveListener(delegate { OnRestart?.Invoke(); });
        _exit.onClick.RemoveListener(delegate { OnExit?.Invoke(); });
    }
}