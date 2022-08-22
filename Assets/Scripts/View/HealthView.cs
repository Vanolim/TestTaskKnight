using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour, IHealthView
{
    [SerializeField] private Slider _slider;

    private int _maxValue;

    public void Init(int maxValue)
    {
        _maxValue = maxValue;
        SetValue(_maxValue);
    }

    public void SetValue(float value) => _slider.value = Mathf.InverseLerp(0, _maxValue, value);
}