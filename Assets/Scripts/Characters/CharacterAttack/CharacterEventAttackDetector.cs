using System;
using UnityEngine;

public class CharacterEventAttackDetector : MonoBehaviour
{
    public event Action OnAttack;
    
    public void Attack() => OnAttack?.Invoke();
}