using System;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private GameUpdate _gameUpdate;
    [SerializeField] private HeroSpawner _heroSpawner;

    private Game _game;

    private void Start()
    {
        _game = new Game(_gameUpdate, _heroSpawner);
    }
}