using UnityEngine;
using System;

public class EnemyDeath : MonoBehaviour
{
  public enum EnemyType
  {
    Normal,
    Shooting,
    Mothership
  }

  public EnemyType Type;

  public event Action<EnemyDeath, int> Died;

  [SerializeField]
  private int _score = 1;

  private void OnTriggerEnter2D(Collider2D other) => 
    Die();

  private void Die() => 
    Died?.Invoke(this, _score);
}
