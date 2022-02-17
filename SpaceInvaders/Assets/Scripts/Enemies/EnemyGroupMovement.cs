using System.Collections.Generic;
using UnityEngine;


public class EnemyGroupMovement : MonoBehaviour
{
  [SerializeField]
  private float _initialSpeed;
  [SerializeField]
  private float _speedStep;

  private float _moveSpeed;
  private float _moveDirrection = 1f;
  private bool _canMove = false;

  private List<Transform> _enemiesList = new List<Transform>();
  private float _xMin, _xMax;  

  private void Update()
  {
    if (_canMove == false) return;

    MoveHorizontal();

    CheckEnemyBoundariesReaching();
  }

  public void StartMovement(List<Transform> groupList)
  {
    _canMove = true;
    _moveSpeed = _initialSpeed;
    _enemiesList = groupList;
    SetUpMoveBoundaries();
  }

  private void CheckEnemyBoundariesReaching()
  {
    foreach (Transform enemy in _enemiesList)
    {
      if (IsDead(enemy)) continue;

      if (NotReachedEdge(enemy)) continue;

      ChangeDirrection();
      AdvanceRow();
      IncreaseSpeed();

      break;
    }
  }

  private bool NotReachedEdge(Transform enemy) => 
    _xMin <= enemy.position.x && enemy.position.x <= _xMax;

  private static bool IsDead(Transform enemy) => 
    enemy.gameObject.activeInHierarchy == false;

  private void IncreaseSpeed() => 
    _moveSpeed += _speedStep;

  private void AdvanceRow() => 
    transform.position = new Vector2(transform.position.x, transform.position.y - 1f);

  private void ChangeDirrection() => 
    _moveDirrection *= -1f;

  private void MoveHorizontal() => 
    transform.position += _moveDirrection * _moveSpeed * Time.deltaTime * Vector3.right;

  private void SetUpMoveBoundaries()
  {
    Sprite enemySprite = _enemiesList[0].gameObject.GetComponent<SpriteRenderer>().sprite;
    float xPadding = enemySprite.rect.size.x / enemySprite.pixelsPerUnit / 2f;

    Camera gameCamera = Camera.main;
    _xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
    _xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
  }
}
