using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  public int LivesRemaining => _playerLives;

  public event Action<PlayerHealth> Died;

  [SerializeField]
  private int _playerLives = 3;

  [SerializeField]
  private LayerMask _enemyLayerMask;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (_playerLives == 0 || IsTouchingEnemyShip(other.gameObject.layer))
      GameOver();
    else
      Die();
  }

  private void Die()
  {
    _playerLives--;
    Died?.Invoke(this);
  }

  private void GameOver()
  {
    _playerLives = 0;
    Die();
    gameObject.SetActive(false);
  }

  private bool IsTouchingEnemyShip(int layer) => 
    (_enemyLayerMask & (1 << layer)) > 0;
}
