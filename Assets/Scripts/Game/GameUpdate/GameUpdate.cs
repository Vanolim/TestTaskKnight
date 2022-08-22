using System;
using System.Collections.Generic;
using UnityEngine;

public class GameUpdate : MonoBehaviour
{
    private List<IUpdateble> _updatebles = new List<IUpdateble>();

    private void Update()
    {
        foreach (var updateble in _updatebles)
        {
            updateble.UpdateState(Time.deltaTime);
        }
    }

    public void Register(IUpdateble updateble)
    {
        _updatebles.Add(updateble);
    }
}