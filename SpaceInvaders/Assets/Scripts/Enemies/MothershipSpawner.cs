using System.Collections;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class MothershipSpawner : MonoBehaviour
{
  public event Action<Mothership> MothershipCreated;

  [Header("Spawner Values")]
  [SerializeField]
  [Range(0f, 1f)]
  private float _spawnChance = 0.4f;
  [SerializeField]
  private float _spwnCheckTime = 4f;
  [SerializeField]
  private Transform[] _spawnPoints;

  [Space]
  [Header("Prefabs")]
  [SerializeField]
  private Mothership _mothershipPrefab;
  [SerializeField]
  private Projectile _projectilePrefab;

  private Mothership _mothershipInstance;
  private ProjectileFactory _projectileFactory;

  private void Awake() => 
    _projectileFactory = new ProjectileFactory(_projectilePrefab, transform);

  private IEnumerator Start()
  {
    while (true)
    {
      yield return new WaitForSeconds(_spwnCheckTime);
      SpawnWithProbability();
    }    
  }

  private void SpawnWithProbability()
  {
    float roll = Random.Range(0f, 1f);

    if (roll >= _spawnChance)
      return;

    SpawnMothersip();
  }

  private void SpawnMothersip()
  {
    int startSpawnPointIndex = Random.Range(0, _spawnPoints.Length);
    int endSpawnPointIndex = 1 - startSpawnPointIndex;

    if (_mothershipInstance == null)
      CreateMothership();

    _mothershipInstance.Move(_spawnPoints[startSpawnPointIndex].position, _spawnPoints[endSpawnPointIndex].position);
  }

  private void CreateMothership()
  {
    _mothershipInstance = Instantiate(_mothershipPrefab, transform);
    _mothershipInstance.GetComponent<EnemyFire>().SetProjectileSource(_projectileFactory);
    MothershipCreated?.Invoke(_mothershipInstance);
  }
}
