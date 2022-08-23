using System;
using UnityEngine;
using UnityEngine.UI;

public class StartView : MonoBehaviour
{
    [SerializeField] private Button _start;
    [SerializeField] private Button _exit;
    
    public event Action OnExit;
    public event Action OnStart;

    public void Activate() => gameObject.SetActive(true);
    public void Deactivate() => gameObject.SetActive(false);

    private void OnEnable()
    {
        _start.onClick.AddListener(delegate { OnStart?.Invoke(); });
        _exit.onClick.AddListener(delegate { OnExit?.Invoke(); });
    }

    private void OnDisable()
    {
        _start.onClick.RemoveListener(delegate { OnStart?.Invoke(); });
        _exit.onClick.RemoveListener(delegate { OnExit?.Invoke(); });
    }
}