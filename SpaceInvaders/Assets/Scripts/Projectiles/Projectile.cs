using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
  public event Action<Projectile> ReleaseEvent;

  public enum MoveDirrection
  {
    Up = 1,
    Down = -1
  }

  [SerializeField]
  private float _moveSpeed;
  [SerializeField]
  private MoveDirrection _moveDirrection;

  private void Update() => 
    Move();

  private void OnTriggerEnter2D(Collider2D collision) => 
    gameObject.SetActive(false);

  private void OnBecameInvisible()
  {
    ReleaseEvent?.Invoke(this);
    gameObject.SetActive(false);
  }

  private void Move() =>
    transform.position += (float)_moveDirrection * _moveSpeed * Time.deltaTime * Vector3.up;
}
