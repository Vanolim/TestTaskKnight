using TMPro;
using UnityEngine;

public class CoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _core;

    private string _initialText = "Scores: ";

    public void UpdateCore(int value) => _core.text = _initialText + value;
}