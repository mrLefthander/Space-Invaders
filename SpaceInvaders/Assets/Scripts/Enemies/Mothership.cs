using UnityEngine;

public class Mothership : MonoBehaviour
{
  [SerializeField]
  private float _moveSpeed = 10f;

  private bool _isMoving = false;
  private Vector3 _targetPosition;

  private void Update()
  {
    if (!_isMoving) return;

    transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);

    if (IsDestinationReached)
      Stop();
  }
  public void Move(Vector2 from, Vector2 to)
  {
    gameObject.SetActive(true);
    SetPositions(from, to);

    if (IsDestinationReached) return;

    _isMoving = true;
  }

  private void Stop()
  {
    _isMoving = false;
    gameObject.SetActive(false);
  }

  private void SetPositions(Vector2 from, Vector2 to)
  {
    transform.position = from;
    _targetPosition = to;
  }

  private bool IsDestinationReached =>
    Vector2.Distance(transform.position, _targetPosition) <= _moveSpeed * Time.deltaTime;
}
