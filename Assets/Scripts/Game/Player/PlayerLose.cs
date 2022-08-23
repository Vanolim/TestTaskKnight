using System;
using UnityEngine;

public class PlayerLose
{
      private Hero _hero;

      public event Action OnLose;
      
      public PlayerLose(Hero hero)
      {
            _hero = hero;
            hero.OnDead += Lose;
      }

      private void Lose()
      {
            Debug.Log("Lose");
            OnLose?.Invoke();
      }
}