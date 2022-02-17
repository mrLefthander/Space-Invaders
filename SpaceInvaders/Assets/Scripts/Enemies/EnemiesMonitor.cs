using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMonitor : MonoBehaviour
{
  public event Action GameWon;

  [Header("Enemies Spawners")]
  [SerializeField]
  private EnemiesGroupSpawner _enemyGroupSpawner;
  [SerializeField]
  private MothershipSpawner _mothershipSpawner;

  [Space]
  [SerializeField]
  private Score _score;

  private Mothership _mothership;
  private List<Transform> _enemyList = new List<Transform>();

  private void Start() => 
    _mothershipSpawner.MothershipCreated += OnMothershipCreated;

  public void SetEnemiesList(List<Transform> enemyList)
  {
    _enemyList = enemyList;
    SubscribeForDeath();
  }

  private void SubscribeForDeath() => 
    _enemyList.ForEach(e => e.GetComponent<EnemyDeath>().Died += OnEnemyDeath);

  private void OnEnemyDeath(EnemyDeath enemy, int score)
  {
    _score.GainScoreFrom(enemy.transform.position, score);

    UnsubscribeFromDeath(enemy);

    CheckGameWin(enemy);
  }

  private void UnsubscribeFromDeath(EnemyDeath enemy)
  {
    if (IsMothership(enemy)) return;

    enemy.Died -= OnEnemyDeath;
  }

  private bool IsMothership(EnemyDeath enemy) => 
    enemy.Type == EnemyDeath.EnemyType.Mothership;

  private void CheckGameWin(EnemyDeath enemy)
  {
    int activeEnemiesInList = CountActive();

    if (IsMothership(enemy) || activeEnemiesInList > 1)  
      return;

    StartCoroutine(WaitWhileMothersipActive());
  }

  private int CountActive() => 
    _enemyList.Count(e => e.gameObject.activeInHierarchy == true);

  private IEnumerator WaitWhileMothersipActive()
  {
    yield return new WaitWhile(IsMothershipActive);
    GameWon?.Invoke();
  }

  private bool IsMothershipActive() => 
    _mothership.gameObject.activeInHierarchy;

  private void OnMothershipCreated(Mothership enemy)
  {
    _mothership = enemy;
    _mothership.GetComponent<EnemyDeath>().Died += OnEnemyDeath;

    _mothershipSpawner.MothershipCreated -= OnMothershipCreated;
  }
}
