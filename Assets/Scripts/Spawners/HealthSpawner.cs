using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    [SerializeField] private Heart _prefabHeart;

    private List<Heart> _activeHeart = new List<Heart>();

    public void Spawn(Vector2 position)
    {
        Heart newHeart = Instantiate(_prefabHeart, position, Quaternion.identity);
        _activeHeart.Add(newHeart);
        newHeart.OnWorked += RemoveHeart;
    }

    public void Deactivate()
    {
        for (int i = 0; i < _activeHeart.Count; i++)
        {
            _activeHeart[i].Destroyed();
        }

        _activeHeart = new List<Heart>();
    }

    private void RemoveHeart(Heart heart)
    {
        heart.OnWorked -= RemoveHeart;
        _activeHeart.Remove(heart);
        heart.Destroyed();
    }
}