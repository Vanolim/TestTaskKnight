using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] private HeroPrefab _heroPrefab;
    [SerializeField] private Transform _spawnPosition;

    public HeroPrefab Spawn() => Instantiate(_heroPrefab, _spawnPosition.position, Quaternion.identity).GetComponent<HeroPrefab>();
}