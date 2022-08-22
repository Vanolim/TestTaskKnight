using UnityEngine;
using UnityEngine.UI;

public class HeroHealthView : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private int _maxValue;

    public void Init(int maxValue)
    {
        _maxValue = maxValue;
    }

    public void SetValue(float value)
    {
        _slider.value = Mathf.InverseLerp(0, _maxValue, value);
    }
}
